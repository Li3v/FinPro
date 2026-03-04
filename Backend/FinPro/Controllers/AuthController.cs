using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace FinPro;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly AuthService _authService;
    private readonly JwtService _jwtService;
    public AuthController(AppDbContext dbContext, AuthService authService, JwtService jwtService) 
    { 
        _dbContext = dbContext; 
        _authService = authService;
        _jwtService = jwtService;
    }

    #region POST - Login
    [HttpPost("login")]
    public Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Name == request.Name);
        if(user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Task.FromResult<IActionResult>(Unauthorized(new{ message = "Invalid Credentials."}));
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = _jwtService.GenerateToken(claims);
        return Task.FromResult<IActionResult>(Ok(new{ access_token = token, token_type = "Bearer"}));
    }
    #endregion

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        await _authService.CreateUser(request);
        return Ok(new { message = "User Created" });
    }
}
