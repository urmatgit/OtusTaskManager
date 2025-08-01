
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UserService.DataAccess.DTOs.Auth;

public class KeycloakUserService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public KeycloakUserService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _config = configuration;
    }

    public async Task<bool> CreateUserAsync(RegisterRequest model)
    {
        var token = await GetAdminTokenAsync();
        if (string.IsNullOrEmpty(token)) return false;

        var user = new
        {
            username = model.Username,
            firstName = model.FirstName,
            lastName = model.LastName,
            email = model.Email,
            enabled = true,
            emailVerified = false,
            attributes = new { phoneNumber = new[] { model.Phone } }
        };

        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{_config["Keycloak:AdminUrl"]}/admin/realms/{_config["Keycloak:Realm"]}/users")
        {
            Content = JsonContent.Create(user)
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    private async Task<string> GetAdminTokenAsync()
    {
        var form = new Dictionary<string, string>
        {
            ["client_id"] = _config["Keycloak:ClientId"],
            ["client_secret"] = _config["Keycloak:ClientSecret"],
            ["grant_type"] = "client_credentials"
        };

        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{_config["Keycloak:AdminUrl"]}/realms/{_config["Keycloak:Realm"]}/protocol/openid-connect/token")
        {
            Content = new FormUrlEncodedContent(form)
        };

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        using var stream = await response.Content.ReadAsStreamAsync();
        var doc = await JsonDocument.ParseAsync(stream);
        return doc.RootElement.GetProperty("access_token").GetString();
    }
}