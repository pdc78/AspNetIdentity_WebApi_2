using AspNetIdentity_WebApi.Infrastructure;
using AspNetIdentity_WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

// http://bitoftech.net/2015/03/11/asp-net-identity-2-1-roles-based-authorization-authentication-asp-net-web-api/

namespace AspNetIdentity_WebApi.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        [Authorize(Roles ="Admin")]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(AppUserManager.Users.ToList().Select(u => TheModelFactory.Create(u)));
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await AppUserManager.FindByIdAsync(id);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Sender = createUserModel.Sender,
                Level = 3,
                JoinDate = DateTime.Now.Date,
                Date_Created = DateTime.Now.Date,
                Active_Flg = true,
            };

            IdentityResult addUserResult = await AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            //Send email Confirmation

            string code = await AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            if (code != null)
            {
                var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code }));

                await AppUserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            }

            // create uri to get info of user

            return Created(new Uri(Url.Link("GetUserById", new {id = user.Id})), TheModelFactory.Create(user));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await AppUserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        [Authorize]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            IdentityResult result = await AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)

            var appUser = await AppUserManager.FindByIdAsync(id);

            if (appUser != null)
            {
                IdentityResult result = await AppUserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();

            }

            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] List<string> rolesToAssign)
        {

            var appUser = await AppUserManager.FindByIdAsync(id);

            // error if userId doesn't exsist
            if (appUser == null)
            {
                return NotFound();
            }

            var currentRoles = await AppUserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = rolesToAssign.Except(AppRoleManager.Roles.Select(x => x.Name)).ToArray();

            // error if passed a role that doesn't exsist
            if (rolesNotExists.Any())
            {

                ModelState.AddModelError("", errorMessage: string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            // Remove all roles associated
            IdentityResult removeResult = await AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            // Add all roles passed as parameter
            IdentityResult addResult = await AppUserManager.AddToRolesAsync(appUser.Id, rolesToAssign.ToArray());

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");

                if (ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }
              
            return Ok();
        }
    }
}
