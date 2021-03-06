﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogezyTeamWork.Data;
using BlogezyTeamWork.Models;
using BlogezyTeamWork.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogezyTeamWork.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BlogezyDbContext _db;
        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, BlogezyDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(loginModel.Email);
                var userRole = await _userManager.IsInRoleAsync(user, "Admin");
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.IsRemmember, true);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("id", user.Id);
                        HttpContext.Session.SetString("name", user.UserName);
                        HttpContext.Session.SetString("isLoged", "true");
                        //return RedirectToAction("Index", "Home", new { area = "" });

                        if (userRole)
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else
                        {
                            HttpContext.Session.SetString("id", user.Id);
                            HttpContext.Session.SetString("name", user.UserName);
                            HttpContext.Session.SetString("isLoged", "true");
                            return RedirectToAction("Index", "Home", new { area = "" });
                        }

                    }

                    return View();
                }

            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = registerModel.Name,
                    Email = registerModel.Email,
                };
                var identityResult = await _userManager.CreateAsync(user, registerModel.Password);
                if (identityResult.Succeeded)
                {
                    HttpContext.Session.SetString("id", user.Id);
                    HttpContext.Session.SetString("name", user.UserName);
                    await _userManager.AddToRoleAsync(user, "user");
                    var signInResult = await _signInManager.PasswordSignInAsync(user, registerModel.Password, true, true);
                    if (signInResult.Succeeded)
                    {
                        HttpContext.Session.SetString("isLoged", "true");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return View();
                }

            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}