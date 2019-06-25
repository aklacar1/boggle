using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BoggleREST.API.ServiceInterfaces;
using BoggleREST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCApp.Services;
using BoggleREST.DataLayer.Models.BindingModels;
using BoggleREST.DataLayer.Models.ViewModels;
namespace BoggleREST.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IUserService userService;
        public UsersController(IUserService userService, UserManager<Users> userManager,SignInManager<Users> signInManager,IEmailSender emailSender)
        {
            this._emailSender = emailSender;
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        /// <summary>
        /// Entire code in Identity Manipulation should be moved to UserService. Same is for all controllers, controller itself should not do much at all.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        #region Identity Manipulation Methods
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]inUserModel model)
        {

            var user = new Users { UserName = model.UserName, Email = model.EMail };
            var result = await userManager.CreateAsync(user, model.Password);

            if(result.Succeeded == true) {
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailConfirmationAsync(model.EMail, callbackUrl);
                await signInManager.SignInAsync(user, isPersistent: false);

                return Ok(callbackUrl);
            }


            // If we got this far, something failed, redisplay form
            return BadRequest(result.Errors);
        }
        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            return Ok(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Reset Passworf Fail");
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest("Reset Passworf Fail");
            }
            var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Reset Password Fail");
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    return Ok();
                }
                if (result.IsLockedOut)
                {
                    return BadRequest("Locked Out");
                }
            }

            return BadRequest(model);
        }
        [HttpGet("Auth")]
        [Authorize]
        public async Task<IActionResult> Auth()
        {
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (String.IsNullOrEmpty(userID)) {
                return BadRequest("User not found");
            }
            Users u = await userManager.FindByIdAsync(userID);
            AuthUserViewModel retVal = new AuthUserViewModel() { UserName = u.UserName, UserId = u.Id,Roles = await userManager.GetRolesAsync(u) as List<string>};
            return Ok(retVal);
        }
        [HttpPost("Logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok("Successfully logged out");
        }
        [HttpPost("AddRoleToUser")]
        [Authorize(Roles ="SU")]
        public async Task<IActionResult> AddRoleToUser(Users user,string role) {
            IdentityResult u = await userManager.AddToRoleAsync(user, role);
            if (u.Succeeded == false) {
                return BadRequest(u.Errors);
            }
            return Ok(u.Succeeded);
        }
        [HttpPost("RemoveUserFromRole")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> RemoveUserFromRole(Users user, string role)
        {
            IdentityResult u = await userManager.RemoveFromRoleAsync(user, role);
            if (u.Succeeded == false)
            {
                return BadRequest(u.Errors);
            }
            return Ok(u.Succeeded);
        }
        #endregion
        #region UserService Methods
        [HttpPost("InsertPerson")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> InsertUser(Users user)
        {
            var returnValue = userService.InsertUser(user);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }

        [HttpPut("UpdateUser")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> UpdateUser(Users user)
        {
            var returnValue = userService.UpdateUser(user);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }

        [HttpGet("GetUserById/{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> GetUserById([FromRoute]string id)
        {
            var returnValue = userService.GetUserById(id);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }

        [HttpGet("GetUserByUsername/{Username}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> GetUserByUsername([FromRoute]string Username)
        {
            var returnValue = userService.GetUserByUsername(Username);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }
        [HttpDelete("DeleteUserById/{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> DeleteUserById([FromRoute]string id)
        {
            var returnValue = userService.DeleteUserById(id);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }

        [HttpDelete("DeleteUserByUserName/{userName}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> DeleteUserByUserName([FromRoute]string userName)
        {
            var returnValue = userService.DeleteUserByUserName(userName);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }
        #endregion
    }
}