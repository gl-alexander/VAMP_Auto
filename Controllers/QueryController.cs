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
    }
}
