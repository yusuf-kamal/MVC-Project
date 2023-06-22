﻿using Demo.DAL.Models;
using Demo.Pl.Helper;
using Demo.Pl.Models;
using Demo.Pl.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Pl.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task <IActionResult> Create(IdentityRole  identityRole)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(identityRole);

            }
            return View(identityRole);
        }



        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            return View(role);
        }





        public async Task<IActionResult> Update(string id)
        {
            if (id is null)
                return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, IdentityRole  identityRole)
        {
            if (id != identityRole.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = identityRole.Name;
                    role.NormalizedName = identityRole.Name.ToUpper();
                    var result = await _roleManager.UpdateAsync(role);
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
            return View(identityRole);

        }



        public async Task<IActionResult> Delete(String id)
        {
            if (id is null)
                return NotFound();
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                var result = await _roleManager.DeleteAsync(role);

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


        public async Task<IActionResult> AddOrRemoveUsers( string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role is null)
                return NotFound();

            ViewBag.RoleId = RoleId;

            var users = new List<UserInRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId=user.Id,

                    UserName=user.UserName
                };

                if(await _userManager.IsInRoleAsync(user,role.Name))
                    userInRole.Isselected = true;
                else
                    userInRole.Isselected = false;
                users.Add(userInRole);

            }

            return View(users);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleViewModel> models, string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role is null)
                return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var item in models)
                {
                    var user = await _userManager.FindByIdAsync(item.UserId);
                    if(user is not null)
                    {
                        if(item.Isselected&&!(await _userManager.IsInRoleAsync(user,role.Name)))
                         await _userManager.AddToRoleAsync(user,role.Name);
                        else if(!item.Isselected&&(await _userManager.IsInRoleAsync(user,role.Name)))
                            await _userManager.RemoveFromRoleAsync(user,role.Name);
                    }
                }
                return RedirectToAction("Update",new {id=RoleId});
            }

            return View(models);

        }


    }
}
