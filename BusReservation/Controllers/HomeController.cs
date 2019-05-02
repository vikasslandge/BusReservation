using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusReservation.Models;

namespace BusReservation.Controllers
{
    public class HomeController : Controller
    {
        BusBookingSystemEntities entities = new BusBookingSystemEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDetails user, FormCollection collection)
        {
            UserDetails detail = new UserDetails();
            detail.UserId = user.UserId;
            detail.Name = collection["name"];
            detail.Password = collection["password"];
            detail.MobileNumber = collection["mobileNumber"];
            detail.Gender = collection["Gender"];
            detail.Email = collection["email"];
            detail.Address = collection["address"];
            detail.DateOfBirth = Convert.ToDateTime(collection["DateOfBirth"]);
            detail.UserType = "User";
 
            entities.UserDetails.Add(detail);
            entities.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Authorize(UserDetails login)
        {
            using (BusBookingSystemEntities entities = new BusBookingSystemEntities())
            {
                var userDetails = entities.UserDetails.Where(user => user.MobileNumber == login.MobileNumber && user.Password == login.Password).FirstOrDefault();
                int loginid = entities.UserDetails.Where(s => s.MobileNumber == userDetails.MobileNumber).Select(s1 => s1.UserId).FirstOrDefault();
                if (userDetails == null)
                {
                    return View("Index", login);
                }
                else
                {
                    string role = userDetails.UserType;
                    if (role == "Admin")
                    {
                        Session["UserId"] = loginid;
                        Session["Name"] = login.Name;
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (role == "User")
                    {
                        Session["UserId"] = loginid;
                        Session["Name"] = login.Name;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult Cancel()
        {
            return View();
        }
        public ActionResult Reschedule()
        {
            return View();
        }
        public ActionResult Print()
        {
            return View();
        }
        public ActionResult Feedback()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            int id = Convert.ToInt32(Session["UserId"]);
            UserDetails user = entities.UserDetails.Find(id);
            return View(user);
        }
        public ActionResult MyTrips()
        {
            int id = Convert.ToInt32(Session["UserId"]);
            return View();
        }
        public ActionResult BusList()
        {
            return View();
        }

        public ActionResult SelectSeat()
        {
            return View();
        }

    }
}