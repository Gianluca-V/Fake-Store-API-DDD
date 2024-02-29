using System.ComponentModel.DataAnnotations;

namespace Contracts.Authentication;

public record RegisterRequest(
    [Required] string Username,
    [Required] string Email,
    [Required] string Password
);