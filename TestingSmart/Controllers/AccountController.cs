using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestingSmart.Models;
using TestingSmart.ViewModels;

namespace TestingSmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
          UserManager<User> userManager,
          SignInManager<User> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [Route("RegisterUser")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Register([FromBody]UserViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new User { UserName = model.UserName, Email = model.Email, Address=model.Address, FullName=model.FullName, PhoneNumber=model.PhoneNumber,UserType=model.UserType   };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    //  await _signInManager.SignInAsync(user, isPersistent: false);
                   
                    return Json(result);
                }
                return Json(result);
            }

            // If we got this far, something failed, redisplay form
            return Json("model is not valid");
        }
        [Authorize]
        [Route("GetUserDetailList")]
        public List<UserViewModel> GetUserDetailList()
        {

            var rolesList = _userManager.Users.Select(t => new UserViewModel()
            {
                Id = t.Id,
                FullName = t.FullName,
                UserName = t.UserName,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber,
                Password = t.PasswordHash,
                Address = t.Address,
                UserType = t.UserType
            }).ToList();
            return rolesList;
        }
        [HttpGet]
        [Route("GetUserDetailById/{id}")]
        public UserViewModel GetUserDetailById(string id)
        {

            var rolesList = _userManager.Users.Where(x=>x.Id==id).Select(t => new UserViewModel()
            {
                Id = t.Id,
                FullName = t.FullName,
                UserName = t.UserName,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber,
                Password = t.PasswordHash,
                Address = t.Address,
                UserType = t.UserType
            }).FirstOrDefault();
            return rolesList;
        }

        
        [Route("UpdateUser")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody]UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDetails = await _userManager.FindByIdAsync(model.Id);
                if(userDetails==null)
                {
                    return null;
                }
                userDetails.UserName = model.UserName;
                userDetails.Email = model.Email;
                userDetails.Address = model.Address;
                userDetails.PhoneNumber = model.PhoneNumber;
                userDetails.Address = model.Address;
                userDetails.UserType = model.UserType;
                var result = await _userManager.UpdateAsync(userDetails);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    //  await _signInManager.SignInAsync(user, isPersistent: false);

                    return Json(result);
                }
                return Json(result);
            }

            // If we got this far, something failed, redisplay form
            return Json("model is not valid");
        }
    }
}