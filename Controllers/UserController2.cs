using Microsoft.AspNetCore.Http;
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
        public IActionResult Index()
        {
            var u = new User();
            u = context.Users.First(x => x.Username == HttpContext.Session.GetString("username"));
            return View(u);
        }

        private readonly AppDbContext context;
        public UserController(AppDbContext _db)
        {
            context = _db;
            //token = new User();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(User user)
        {
            
            if(context.Users.Any(x => x.Username == user.Username))
            {
                
                if(context.Users.Any(x=>x.Password==user.Password))
                {
                    user = context.Users.First(x => x.Username == user.Username);
                    HttpContext.Session.SetString("username", user.Username);
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
                if(!context.Users.Any(x=>x.Username==user.Username) && !context.Users.Any(x => x.UCN == user.UCN) && !context.Users.Any(x => x.Email == user.Email))
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("LogIn");
                }
                else
                {
                    return RedirectToAction("Register");
                }
                
            }
            return View();
        }

        [HttpPost]
        public IActionResult LogOut(User user)
        {
            HttpContext.Session.SetString("username",null);
            return RedirectToAction("LogIn");
        } 
    }
}
