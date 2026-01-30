using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace _Library_Management_System_.Controllers
{
    /// <summary>
    /// Handles authentication-related operations such as user registration and login.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtTokenGenerator _jwt;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Initializes a new instance of the AuthController class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    /// <param name="jwt">The JWT token generator service.</param>
    /// <param name="logger">The logger instance.</param>
    public AuthController(AppDbContext context, JwtTokenGenerator jwt, ILogger<AuthController> logger)
    {
        _context = context;
        _jwt = jwt;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            return BadRequest("Email and password are required");

        if (_context.Users.Any(u => u.Email == request.Email))
            return BadRequest("User already exists");

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = PasswordHelper.HashPassword(request.Password),
            Role = request.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully");
    }


    /// <summary>
    /// Authenticates a user and returns a JWT token upon successful login.
    /// </summary>
    /// <param name="request">The login request containing email and password.</param>
    /// <returns>An IActionResult containing the JWT token or an error message.</returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            return BadRequest("Email and password are required");

        _logger.LogInformation("Login attempt for email: {Email}", request.Email);

        var user = _context.Users.FirstOrDefault(x => x.Email == request.Email);
        if (user == null ||
            !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
        {
            _logger.LogWarning("Invalid login attempt for email: {Email}", request.Email);
            return Unauthorized("Invalid credentials");
        }

        var token = _jwt.GenerateToken(user);

        _logger.LogInformation("Login successful for email: {Email}", request.Email);
        return Ok(new { token });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminEndpoint()
    {
        return Ok("Admin access granted");
    }

    [Authorize(Roles = "User,Admin")]
    [HttpGet("user")]
    public IActionResult UserEndpoint()
    {
        return Ok("User access granted");
    }
    }
}
