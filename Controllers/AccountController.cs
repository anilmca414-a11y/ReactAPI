using DemoDotNetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace DemoDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtService;
        public AccountController(UserManager<IdentityUser> userManager,JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult Secure()
        {
            return Ok("You are logged in");
        }

        // POST api/<AccountController>
        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
                return Ok("User created");
            return BadRequest(result.Errors);
        }
        [EnableCors]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
          [FromBody] LoginRequest request,
        SignInManager<IdentityUser> signInManager)
        {
            var result = await signInManager.PasswordSignInAsync(
                request.Email, request.Password, false, false);
            if (result.Succeeded)
            {
                var token = _jwtService.GenerateToken(request.Email);
                //return Ok(new { token });
                return Content("{ \"message\": \"Login successful\", \"status\": true ,\"token\": \"" + token + "\"}", "application/json");
            }
            return Unauthorized();
        }
    }
}
