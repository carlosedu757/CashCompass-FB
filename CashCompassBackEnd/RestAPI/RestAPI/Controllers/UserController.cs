using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using System.Security.Claims;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }


    [HttpGet]
    public ActionResult<string> Get()
    {
        return "UserController :: Acessado em : " + DateTime.Now.ToLongDateString();
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] User model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
        }

        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return Ok(model);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] Login userInfo)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login Inválido...");
                return BadRequest(ModelState);
            }

            return Ok(userInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Erro durante o processo de login");
        }
    }

    [HttpGet("userinfo")]
    public IActionResult GetUserInfo()
    {
        // Verifique se o usuário está autenticado
        if (User.Identity?.IsAuthenticated ?? false)
        {
            // Recupere as informações do usuário atualmente autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o userId do usuário autenticado
            var userName = User.Identity.Name; // Obtém o nome do usuário autenticado

            var userInfo = new
            {
                UserId = userId,
                UserName = userName
                // Adicione mais informações, se necessário
            };

            return Ok(userInfo);
        }

        return Unauthorized("Usuário não autenticado");
    }
}