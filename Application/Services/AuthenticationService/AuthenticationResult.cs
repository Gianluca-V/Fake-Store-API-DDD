using Domain.UserAggregate;

namespace Application.Services.AuthenticationService;

public record AuthenticationResult(
    User user,
    string Token
);