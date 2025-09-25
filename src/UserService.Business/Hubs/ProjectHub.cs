// Hubs/ProjectHub.cs
using Microsoft.AspNetCore.SignalR;

public class ProjectHub : Hub
{
    private static readonly Dictionary<string, UserInfo> _userConnections = new();

    public class UserInfo
    {
        public string UserId { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public List<string> Groups { get; set; } = new();
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("ReceiveConnectionId", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (_userConnections.TryGetValue(Context.ConnectionId, out var userInfo))
        {
            // Выход из всех групп
            foreach (var group in userInfo.Groups)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            }
            _userConnections.Remove(Context.ConnectionId);
        }
        await base.OnDisconnectedAsync(exception);
    }

    public string GetConnectionId()
    {
        return Context.ConnectionId;
    }

    public async Task<bool> JoinAsAdmin(string adminId)
    {
        if (string.IsNullOrEmpty(adminId))
        {
            await Clients.Caller.SendAsync("Error", "Admin ID is required");
            return false;
        }

        try
        {
            // Выход из предыдущих групп
            if (_userConnections.TryGetValue(Context.ConnectionId, out var existingInfo))
            {
                foreach (var group in existingInfo.Groups)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
                }
            }

            // Вход в группу администраторов
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");

            _userConnections[Context.ConnectionId] = new UserInfo
            {
                UserId = adminId,
                IsAdmin = true,
                Groups = { "Admin" }
            };

            await Clients.Caller.SendAsync("ConfirmJoin", $"Вы вошли как администратор: {adminId}");
            await Clients.Group("Admin").SendAsync("UserJoined", new { UserId = adminId, IsAdmin = true });

            return true;
        }
        catch (Exception ex)
        {
            await Clients.Caller.SendAsync("Error", $"Ошибка входа как администратор: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> JoinAsUser(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            await Clients.Caller.SendAsync("Error", "User name is required");
            return false;
        }

        try
        {
            // Выход из предыдущих групп
            if (_userConnections.TryGetValue(Context.ConnectionId, out var existingInfo))
            {
                foreach (var group in existingInfo.Groups)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
                }
            }

            // Вход в группу пользователя
            await Groups.AddToGroupAsync(Context.ConnectionId, userName);

            _userConnections[Context.ConnectionId] = new UserInfo
            {
                UserId = userName,
                IsAdmin = false,
                Groups = { userName }
            };

            await Clients.Caller.SendAsync("ConfirmJoin", $"Вы вошли как пользователь: {userName}");
            return true;
        }
        catch (Exception ex)
        {
            await Clients.Caller.SendAsync("Error", $"Ошибка входа как пользователь: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> JoinProject(string projectId)
    {
        if (string.IsNullOrEmpty(projectId))
        {
            await Clients.Caller.SendAsync("Error", "Project ID is required");
            return false;
        }

        try
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, projectId);

            if (_userConnections.TryGetValue(Context.ConnectionId, out var userInfo))
            {
                userInfo.Groups.Add(projectId);
            }

            await Clients.Caller.SendAsync("ConfirmJoin", $"Вы присоединились к проекту: {projectId}");
            await Clients.Group(projectId).SendAsync("UserJoinedProject", new { ProjectId = projectId, UserId = Context.ConnectionId });

            return true;
        }
        catch (Exception ex)
        {
            await Clients.Caller.SendAsync("Error", $"Ошибка присоединения к проекту: {ex.Message}");
            return false;
        }
    }

    public async Task SendTestNotification()
    {
        try
        {
            // Тестовая отправка во все группы
            await Clients.Group("Admin").SendAsync("ReceiveNotification", new
            {
                Type = "Test",
                Title = "Тестовое уведомление",
                Message = "Это тестовое сообщение для администраторов",
                Timestamp = DateTime.UtcNow
            });

            if (_userConnections.TryGetValue(Context.ConnectionId, out var userInfo))
            {
                await Clients.Group(userInfo.UserId).SendAsync("ReceiveNotification", new
                {
                    Type = "Test",
                    Title = "Тестовое уведомление",
                    Message = $"Это тестовое сообщение для пользователя {userInfo.UserId}",
                    Timestamp = DateTime.UtcNow
                });
            }

            await Clients.Caller.SendAsync("TestMessageSent", "Тестовые сообщения отправлены");
        }
        catch (Exception ex)
        {
            await Clients.Caller.SendAsync("Error", $"Ошибка отправки теста: {ex.Message}");
        }
    }
}