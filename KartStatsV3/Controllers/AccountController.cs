using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KartStatsV3.BLL;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.AspNetCore.Http;
using KartStatsV3.DAL;
using KartStatsV3.Models;
using KartStatsV3.BLL.Interfaces;

namespace KartStatsV3.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userService.Authenticate(model.Username, model.Password))
                {
                    int id = (int)_userService.GetIdByUsername(model.Username);
                    HttpContext.Session.SetString("Username", model.Username);
                    HttpContext.Session.SetInt32("Id", id);
                    return RedirectToAction("SecurePage", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Ongeldige gebruikersnaam of wachtwoord.");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User(model.Username, model.Email);

                _userService.RegisterUser(newUser, model.Password);

                return RedirectToAction("Login");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("Username");

            return RedirectToAction("Login");
        }


    }
}