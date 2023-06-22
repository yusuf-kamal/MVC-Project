using Demo.DAL.Models;
using Demo.Pl.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Pl.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> Index( string SearchValue="")
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var users = _userManager.Users.ToList();
                return View(users);
            }
            else
            {
                var user=await _userManager.FindByEmailAsync(SearchValue);
                return View(new List<ApplicationUser> { user});
            }
        }


        public async Task<IActionResult>Details(string id)
        {
            if(id is null)
                return NotFound();
            var user= await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            return View(user);
        }

        public async Task<IActionResult>Update(string id)
        {
            if (id is null)
                return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id , ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.UserName = applicationUser.UserName;
                    user.NormalizedUserName= applicationUser.UserName.ToUpper();
                    user.PhoneNumber=applicationUser.PhoneNumber;
                    var result= await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View();

        }


      

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete( String id)
        {
            if (id is null)
                return NotFound();
            try
            {
                var user=await _userManager.FindByIdAsync(id);
                var result=await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("Index");
        }
        
    }


}
