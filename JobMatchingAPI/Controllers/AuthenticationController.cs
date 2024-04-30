using JobMatchingAPI.DTO;
using JobMatchingAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobMatchingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<User> userManager , RoleManager<IdentityRole> roleManager , IConfiguration configuration )
        {
            _userManager = userManager;
            _rolemanager = roleManager;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                //if modelstate is okay.
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { status ="Error", message ="Incorrect informations" });
                }
                //if user exist
                var exist = await _userManager.FindByNameAsync(model.Email);
                if (exist != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { status ="Error", message ="User Exist" });
                }
                //add new user
                var newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true,
                    UserName = model.Email,
                    NormalizedEmail = model.Email,
                    NormalizedUserName = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),




                };

                //crete user
                var createuser = await _userManager.CreateAsync(newUser, model.Password);
                //if successful
                if (createuser.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { status = "Error", message = " could not create user" });
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }


            return StatusCode(StatusCodes.Status200OK, new Response { status = "Success", message = "OK" });

            //return Ok (new Response { status = "Success", message = "OK" });

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //if user exist
            var exist = await _userManager.FindByNameAsync(model.Email);
            //if user match password
             if (exist != null && await _userManager.CheckPasswordAsync(exist,model.password))
            {
                var userrole = await _userManager.GetRolesAsync(exist);
                var authclaims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, exist.Email),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach (var user in userrole)
                {
                    authclaims.Add(new Claim(ClaimTypes.Role, user));
                }

                var authsigninkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(5),
                    claims: authclaims,
                    signingCredentials: new SigningCredentials(authsigninkey, SecurityAlgorithms.HmacSha256));


                return Ok(new
                { 
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiraton = token.ValidTo
                });
            }
            return Unauthorized();
           
        }

    }
}
