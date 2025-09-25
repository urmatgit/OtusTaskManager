using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Users;

namespace UserService.Business.Application
{
    public static class Extensions
    {
        public static async Task SendNotificatonToParticipants<THub>(this IHubContext<THub> hubContext,string method,  string projecName, string owner, ICollection<UserResponse> user) where THub : Hub
        {
            // Send specific notification to participants
            foreach (var participant in user)
            {
                await hubContext.Clients.Group(participant.UserName)
                    .SendAsync(method,
                        new { ProjectName = projecName, InvitedBy = owner });
            }
        }
    }
}
