namespace Application.Services.AuthenticationService;

public interface IAuthenticationService{
    Task<AuthenticationResult> Register(string Username, string Email, string Password);

    Task<AuthenticationResult> Login(string Email, string Password);
}