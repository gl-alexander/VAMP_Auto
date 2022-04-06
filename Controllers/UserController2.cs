using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAMPAutoCore.Data;
using VAMPAutoCore.Models;

namespace VAMPAutoCore.Controllers
{
    public class UserController : Controller
    {
        User token;
        public IActionResult Index()
        {
            return View();
        }

        private readonly AppDbContext context;
        public UserController(AppDbContext _db)
        {
            context = _db;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(string username, string password)
        {
            if(context.Users.Any(x => x.Username == username))
            {
                User user = context.Users.First(x => x.Username == username);
                if(user.Password == password)
                {
                    token = user;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
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

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
