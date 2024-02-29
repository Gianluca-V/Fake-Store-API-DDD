namespace Application.Services.AuthenticationService;

public interface IAuthenticationService{
    AuthenticationResult Register(string Username, string Email, string Password);

    AuthenticationResult Login(string Email, string Password);
}