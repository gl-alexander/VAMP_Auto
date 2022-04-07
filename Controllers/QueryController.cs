using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAMPAutoCore.Data;
using VAMPAutoCore.Models;

namespace VAMPAutoCore.Controllers
{
    public class QueryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly AppDbContext context;
        public QueryController(AppDbContext _db)
        {
            context = _db;
        }

        public IActionResult PickDates()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PickDates(Query dates)
        {
            HttpContext.Session.SetString("queryStartDate", dates.StartDate.ToString());
            HttpContext.Session.SetString("queryEndDate", dates.EndDate.ToString());
            return RedirectToAction("ShowAvailableCars");
        }

        public IActionResult ShowAvailableCars()
        {
            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString("queryStartDate"));
            DateTime endDate = Convert.ToDateTime(HttpContext.Session.GetString("queryEndDate"));

            List<Car> availableCars = new List<Car>();
            foreach (Car car in context.Cars)
            {
                availableCars.Add(car);

                if (context.Queries.Any(x => x.CarId == car.CarId))
                {
                    foreach (Query q in context.Queries.ToList())
                    {
                        if (q.CarId == car.CarId)
                        {
                            if (!(startDate < q.StartDate && endDate < q.StartDate) || (startDate > q.EndDate && endDate > q.EndDate))
                            {
                                int carIndex = availableCars.IndexOf(car);
                                availableCars.RemoveAt(carIndex);
                            }
                        }
                    }
                }


            }
            return View(availableCars);
        }
        
        public IActionResult Reserve(int id)
        {
            Car car = context.Cars.First(x => x.CarId == id);
            Query query = new Query();
            query.CarId = id;
            query.Car = car;
            this.session.SetString("queryCarId", id.ToString());
            return View(query);
        }

        [HttpPost]
        public IActionResult Reserve(Query query)
        {
            DateTime startDate = Convert.ToDateTime(this.session.GetString("queryStartDate"));
            DateTime endDate = Convert.ToDateTime(this.session.GetString("queryEndDate"));
            int carId = int.Parse(this.session.GetString("queryCarId"));
            Car selectedCar = context.Cars.First(x => x.CarId == carId);
            User currenUser = context.Users.First(x => x.Username == HttpContext.Session.GetString("username"));

            query.User = currenUser;
            query.Car = selectedCar;
            query.UserId = currenUser.UserId;
            query.CarId = carId;
            query.StartDate = startDate;
            query.EndDate = endDate;
            if (ModelState.IsValid)
            {
                context.Queries.Add(query);
                context.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("ShowAvailableCars");
        }
    }
}
