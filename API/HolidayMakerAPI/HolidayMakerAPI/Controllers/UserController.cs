using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HolidayMakerAPI.Data;
using HolidayMakerAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Classification;

namespace HolidayMakerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HolidayMakerAPIContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserController(UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager, 
                              HolidayMakerAPIContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }

        // POST: api/User
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.User.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        //}


        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var _user = new IdentityUser {NormalizedUserName = $"{user.FirstName} {user.LastName}", UserName = user.Email, Email = user.Email};
            var result = await userManager.CreateAsync(_user, user.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(_user, isPersistent: false);
                return RedirectToAction();
            }

            return Ok(user);
        }

        public class LoginRequest
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginRequest request)
        {
            var result = await signInManager.PasswordSignInAsync(request.email, request.password, false, false);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Forbid();
        }
    }
}
