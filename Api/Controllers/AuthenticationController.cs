using Application.Services.AuthenticationService;
using Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using MapsterMapper;

namespace Api.Controllers;

[ApiController]
[Route("Auth")]
public class AuthenticationController : ControllerBase{
    private readonly IAuthenticationService authenticationService;
    private readonly IMapper mapper;

    public AuthenticationController(IAuthenticationService AuthenticationService, IMapper Mapper)
    {
        authenticationService = AuthenticationService;
        mapper = Mapper;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request){
        if (!ModelState.IsValid) return BadRequest();
        var authResult = await authenticationService.Register(request.Username, request.Email, request.Password);
        var response = mapper.Map<AuthenticationResponse>(authResult);

        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request){
        if (!ModelState.IsValid) return BadRequest();
        var authResult = await authenticationService.Login(request.Email, request.Password);
        var response = mapper.Map<AuthenticationResponse>(authResult);

        return Ok(response);
    }
}