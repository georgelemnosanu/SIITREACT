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
     
    //logare user
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string username, string password)
    {
        var (user, token) = await _userService.AuthenticateAndGenerateToken(username, password);
        if (user == null)
        {
            return BadRequest(new { message = "Username or password is incorrect" });
        }

        return Ok(new { token, userName = user.UserName, role = "user", userID = user.Id });
    }
//logout logica de implementat
[HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok(new { message = "Logout successful" });
    }

    //Register user
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
    //register admin
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
    //Pagina admin
    [HttpGet("admin")]
    [AllowAnonymous]
    public IActionResult AdminPage()
    {
        return Ok("Welcome to the admin page!");
    }
    //Pagina User
    [HttpGet("user")]
    [AllowAnonymous]
    public IActionResult UserPage()
    {
        return Ok("Welcome to the user page!");
    }
}
