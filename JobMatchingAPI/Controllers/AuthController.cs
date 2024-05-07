using JobMatchingAPI.DTO;
using JobMatchingAPI.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace JobMatchingAPI.Controllers
{
    [Route("api/auth")] //api/Authentication/endpoint
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _rolemanager = roleManager;
            _configuration = configuration;
        }


        [HttpPost("register")]  //api/Auth/register
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { status = "Error", message = "Incorrect information" });
                }

                var existingUser = await _userManager.FindByNameAsync(model.Email);
                if (existingUser != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { status = "Error", message = "User already exists" });
                }

                var newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    UserName = model.Email,
                    NormalizedEmail = model.Email,
                    NormalizedUserName = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var createUserResult = await _userManager.CreateAsync(newUser, model.Password);

                if (!createUserResult.Succeeded)
                {
                    // Concatenate the identity errors into a single error message
                    var errorMessage = string.Join("; ", createUserResult.Errors.Select(e => e.Description));
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { status = "Error", message = $"Could not create user: {errorMessage}" });
                }

                //_logger.LogInformation($"User created successfully: {newUser.UserName}");

                return StatusCode(StatusCodes.Status200OK, new Response { status = "Success", message = "User created successfully" });
            }
            catch (Exception ex)
            {
                //_logger.LogError($"An error occurred during user registration: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { status = "Error", message = "An error occurred during user registration. Please try again later." });
            }

          
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            try
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                if (user == null || !await _userManager.CheckPasswordAsync(user, model.password.Trim()))
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return Unauthorized(new { LoginError = "Invalid username or password." });
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenExpirationInMinutes"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                //_logger.LogError($"An error occurred during login: {ex.Message}");

                // Return a generic error message to the client
                ModelState.AddModelError("", "An error occurred during login. Please try again later.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = "An error occurred during login. Please try again later." });
            }
           
        }
    }
}
