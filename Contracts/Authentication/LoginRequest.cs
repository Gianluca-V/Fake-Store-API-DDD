using System.ComponentModel.DataAnnotations;

namespace Contracts.Authentication;

public record LoginRequest(
    [Required] string Email,
    [Required] string Password
);