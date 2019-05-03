﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusReservation.Models;

namespace BusReservation.Controllers
{
    public class HomeController : Controller
    {
        ClientServiceReference.ClientWebServiceSoapClient soapClient = new ClientServiceReference.ClientWebServiceSoapClient();
        BusBookingSystemEntities entities = new BusBookingSystemEntities();
        public ActionResult Index()
        {
            ViewBag.Source = new SelectList(soapClient.GetCityDetails(), "CityId", "CityName");
            ViewBag.Destination = new SelectList(soapClient.GetCityDetails(), "CityId", "CityName");
            return View();
        }
        [HttpPost]
        public ActionResult BusList(FormCollection collection)
        {
           
            int source = Convert.ToInt32(collection["Source"]);
            string sourceName = soapClient.GetCityDetails().Where(id => id.CityId.Equals(source)).Select(name => name.CityName).FirstOrDefault();
            ViewBag.source = sourceName;

            int destination = Convert.ToInt32(collection["Destination"]);   
            string destinationName = soapClient.GetCityDetails().Where(id => id.CityId.Equals(destination)).Select(name => name.CityName).FirstOrDefault();
            ViewBag.destination = destinationName;


            DateTime doj = Convert.ToDateTime(collection["DateOfJourney"]).Date;
            ViewBag.doj = doj;
            //var result = soapClient.GetRouteDetails(Convert.ToInt32(collection["Source"]), Convert.ToInt32(collection["Destination"]), Convert.ToDateTime(collection["DateOfJourney"]));
            return View(soapClient.GetRouteDetails(source, destination, doj));
        }
        public ActionResult SelectSeat(int id)
        {
            var result=soapClient.GetRouteById(id).Single();

            int source = result.SourceId;
            string sourceName = soapClient.GetCityDetails().Where(ci => ci.CityId.Equals(source)).Select(name => name.CityName).FirstOrDefault();
            ViewBag.source = sourceName;

            int destination = result.DestinationId;
            string destinationName = soapClient.GetCityDetails().Where(ci => ci.CityId.Equals(destination)).Select(name => name.CityName).FirstOrDefault();
            ViewBag.destination = destinationName;

            TimeSpan departure =Convert.ToDateTime(result.DepartureTime).TimeOfDay;
            ViewBag.departure = departure;

            TimeSpan arrival = Convert.ToDateTime(result.ArrivalTime).TimeOfDay;
            ViewBag.arrival = arrival;

            double price =result.Price;
            ViewBag.price = price;
            
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDetails user, FormCollection collection)
        {
            UserDetails detail = new UserDetails();
            //detail.UserId = user.UserId;
            detail.Name = collection["name"];
            detail.Password = collection["password"];
            detail.MobileNumber = collection["mobileNumber"];
            detail.Gender = collection["Gender"];
            detail.Email = collection["Email"];
            detail.Address = collection["Address"];
            detail.DateOfBirth = Convert.ToDateTime(collection["DateOfBirth"]).Date;
            detail.UserType = "User";

            entities.UserDetails.Add(detail);
            entities.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Authorize(UserDetails login,FormCollection collection)
        {
            using (BusBookingSystemEntities entities = new BusBookingSystemEntities())
            {
                string num = login.MobileNumber;
                string pwd = login.Password;

                var userDetails = entities.UserDetails.Where(user => user.MobileNumber.Equals(num) && user.Password.Equals(pwd)).FirstOrDefault();
                //int loginid = entities.UserDetails.Where(s => s.MobileNumber == userDetails.MobileNumber).Select(s1 => s1.UserId).FirstOrDefault();
                int loginid = userDetails.UserId;
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

       

    }
}