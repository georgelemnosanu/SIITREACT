using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SIITREACT.Model;
using SIITREACT.Service;
using Newtonsoft.Json;
using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
   

    public UserController(UserService userService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userService = userService;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    /*
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userService.Authenticate(username, password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            // Get roles of the user
            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();
            var userID = await _userManager.GetUserIdAsync(user);


            return Ok(new { user.UserName, Role = userRole ,UserID=userID }); // Return username and role
        }*/

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _userService.Authenticate(username, password);
        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        // Authentication successful, generate JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key-with-at-least-32-characters"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, "user"),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { token = tokenString, userName = user.UserName, role = "user", userID = user.Id });
    }



    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(string username, string email, string password)
    {
        var result = await _userService.CreateUser(username, email, password, "user");
        if (result.Succeeded)
            return Ok("User created successfully");
        else
            return BadRequest(result.Errors);
    }

    [HttpPost("createadmin")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAdmin(string username, string email, string password)
    {
        var result = await _userService.CreateUser(username, email, password, "admin");
        if (result.Succeeded)
            return Ok("Admin user created successfully");
        else
            return BadRequest(result.Errors);
    }

    [HttpGet("admin")]
    [AllowAnonymous]
    public IActionResult AdminPage()
    {
        return Ok("Welcome to the admin page!");
    }

    [HttpGet("user")]
    [AllowAnonymous]
    public IActionResult UserPage()
    {
        return Ok("Welcome to the user page!");
    }
}
