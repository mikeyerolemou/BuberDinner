using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(ILogger<AuthenticationController> logger,
        IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }

    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = _authenticationService.Register(
            request.Firstname,
            request.Lastname,
            request.Email,
            request.Password);
        
        var response = new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token);

        return Ok(response);
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = _authenticationService.Login(
            request.Email,
            request.Password);

        var response = new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token);


        return Ok(response);
    }
}