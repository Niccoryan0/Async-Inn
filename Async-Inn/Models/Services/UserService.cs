using Async_Inn.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class UserService
    {
        // Utilize this service for authorization
        private AsyncInnDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AsyncInnDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Just an example from class, doesnt really do much
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public bool ValidateUser(List<Claim> claims)
        {
            var nameClaim = claims.FirstOrDefault(x => x.Type == "FirstName").Value;

            if(nameClaim == "Nicco")
            {
                // do something
            }

            return true;
        }
    }
}
