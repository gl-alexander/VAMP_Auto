using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vampCore.Models;

namespace VAMPAutoCore.Controllers
{
    public class UserController : Controller
    {
        User token;
        public IActionResult Index()
        {
            return View(token);
        }

        private readonly AppDbContext context;
        public UserController(AppDbContext _db)
        {
            context = _db;
            token = new User();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(User user)
        {
            string username = user.Username;
            string password = user.Password;
            
            if(context.Users.Any(x => x.Username == user.Username))
            {
                
                if(context.Users.Any(x=>x.Password==user.Password))
                {
                    token = user;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("LogIn");
                }
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("LogIn");
            }
            return View();
        }
    }
}
