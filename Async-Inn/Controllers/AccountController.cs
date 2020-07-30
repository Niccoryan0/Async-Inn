using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "HigherUps")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _config;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = register.Email,
                UserName = register.UserName,
                FirstName = register.FirstName,
                LastName = register.LastName
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, register.Role);
                await _signInManager.SignInAsync(user, false);
            }
            return BadRequest("Invalid registration");
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (result.Succeeded)
            {
                // Look em up
                var user = await _userManager.FindByNameAsync(login.UserName);
                var identityRole = await _userManager.GetRolesAsync(user);
                // Make em a token
                var token = CreateToken(user, identityRole.ToList());

                return Ok(new
                {
                    jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return BadRequest("Invalid attempt");
        }

        [Authorize(Policy = "DistrictManagerOnly")]
        [HttpPost, Route("Assign/Role")]
        public async Task AssignRoleToUser(AssignRoleDTO assignment)
        {
            var user = await _userManager.FindByNameAsync(assignment.UserName);
            //string role;
            //switch (assignment.Role.ToUpper())
            //{
            //    case "DistrictManager":
            //        role = ApplicationRoles.DistrictManager;
            //        break;
            //    case "PROPERTYMANAGER":
            //        role = ApplicationRoles.PropertyManager;
            //        break;
            //    case "AGENT":
            //        role = ApplicationRoles.Agent;
            //        break;
            //    default:
            //        role = ApplicationRoles.Customer;
            //        break;
            //};

            await _userManager.AddToRoleAsync(user, assignment.Role);
        }

        private JwtSecurityToken CreateToken(ApplicationUser appUser, List<string> role)
        {
            // Token needs info called "Claims" which tell something True abou the user. I.e. I have red hair, my name is Nicholas, etc..
            // A User is the "principle" and can have many forms of "identity" containing these claims i.e. birth cert, drivers license, SSN, etc...
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                // Optional:
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("FirstName", appUser.FirstName),
                new Claim("LastName", appUser.LastName),
                new Claim("UserId", appUser.Id),
            };

            foreach (var item in role)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item));
            }
            var token = AuthenticateToken(authClaims);
            return token;
        }

        private JwtSecurityToken AuthenticateToken(List<Claim> claims)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTKey"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWTIssuer"],
                expires: DateTime.UtcNow.AddHours(24),
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
