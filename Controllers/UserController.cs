using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAMP_Auto.Data;
using VAMP_Auto.Models;

namespace VAMP_Auto.Controllers
{
    public class UserController : Controller
    {
        bool loggedIn = false;
        private int currentUserId = -1;
        const int ADMIN_ID = 1;
        public IActionResult Index()
        {
            return View();
        }
        public string SignUp(string username, string key, string firstName, string lastName, string ucn, string email, string phone)
        {
            User user = new User() { Username = username, Password = key };
            try
            {
                using (var context = new AppDbContext())
                {
                    if (context.Users.Any(x => x.Username == username))
                    {
                        return "Username already exists";
                    }
                    else if (string.IsNullOrEmpty(key))
                    {
                        return "Passwords cannot be empty strings";
                    }
                    else if (context.Users.Any(x => x.UCN == ucn))
                    {
                        return "UCN must be unique";
                    }
                    else if (context.Users.Any(x => x.Email == email))
                    {
                        return "Email must be unique";
                    }
                    else
                    {
                        user.FirstName = firstName;
                        user.LastName = lastName;
                        user.UCN = ucn;
                        user.Email = email;
                        user.Phone = phone;
                        context.Users.Add(user);
                        context.SaveChanges();
                        return "Done!";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string LogIn(string username, string key)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    if (context.Users.Any(x => x.Username == username))
                    {
                        var user = context.Users.First(x => x.Username == username);
                        if (user.Password == key)
                        {
                            currentUserId = user.UserId;
                            loggedIn = true;
                            return "Done!";
                        }
                        else return "Invalid password";
                    }
                    else return "Invalid username";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void LogOut()
        {
            this.currentUserId = -1;
            loggedIn = false;
        }


        List<Car> availableCars = new List<Car>();
        public string ShowCars(DateTime start, DateTime end)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    availableCars.Clear();

                    foreach(Car car in context.Cars)
                    {
                        availableCars.Add(car);

                        if (context.Queries.Any(x => x.CarId == car.CarId))
                        {
                            foreach(Query q in context.Queries)
                            {
                                if (q.CarId == car.CarId) 
                                {
                                    if (!(start < q.StartDate && end < q.StartDate) || (start > q.EndDate && end > q.EndDate))
                                    {
                                        int carIndex = availableCars.IndexOf(car);
                                        availableCars.RemoveAt(carIndex);
                                    }
                                }
                            }
                        }
                        
                        
                    }
                    return "Cars were added successfully";

                } 
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string CreateQuery(int carId, DateTime start, DateTime end)
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    Car car = context.Cars.First(x => x.CarId == carId);
                    User user = context.Users.First(x => x.UserId == currentUserId);
                    Query query = new Query();
                    query.CarId = carId;
                    query.Car = car;
                    query.UserId = currentUserId;
                    query.User = user;
                    query.StartDate = start;
                    query.EndDate = end;
                    context.Queries.Add(query);
                    context.SaveChanges();
                    return "Query Created Successfully";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string ShowQueries()
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    List<Query> allQueries = new List<Query>();
                    List<Query> userQueries = new List<Query>();
                    foreach(var query in context.Queries)
                    {
                        allQueries.Add(query);
                        if(query.UserId == currentUserId)
                        {
                            userQueries.Add(query);
                        }
                    }
                    
                    return "Done!";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
