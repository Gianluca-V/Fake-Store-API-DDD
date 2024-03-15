using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Common.Exceptions;
using Domain.UserAggregate;
namespace Application.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJWTTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public AuthenticationService(IJWTTokenGenerator JWTTokenGenerator, IUserRepository UserRepository)
    {
        jwtTokenGenerator = JWTTokenGenerator;
        userRepository = UserRepository;
    }

    public async Task<AuthenticationResult> Register(string Username, string Email, string Password)
    {
        var user = User.Create(Username,Email,Password);

        if (await userRepository.GetUserByEmail(Email) is not null)
        {
            throw new AlreadyExistException("User with given email already exist");
        }

        await userRepository.Add(user);

        var token = jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(user, token);
    }

    public async Task<AuthenticationResult> Login(string Email, string Password)
    {
        if (await userRepository.GetUserByEmail(Email) is not User user || user.Password != Password)
        {
            throw new LoginException("Invalid user credentials");
        }

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}