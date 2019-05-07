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
        ClientServiceReference.ClientWebServiceSoapClient soapClient = new ClientServiceReference.ClientWebServiceSoapClient();
        BusBookingSystemEntities entities = new BusBookingSystemEntities();
        BusHireEntities entity = new BusHireEntities();
        public ActionResult Index()
        {
            ViewBag.Source = new SelectList(soapClient.GetCityDetails(), "CityId", "CityName");
            ViewBag.Destination = new SelectList(soapClient.GetCityDetails(), "CityId", "CityName");

            DateTime date = DateTime.Now;
            TempData["currentDate"] = date.Year + "-0" + date.Month + "-0" + date.Day;

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

            ViewBag.OfferCode = entities.OfferDetails.Where(e => e.ExpiryDate > DateTime.Now).Select(s =>  s.OfferCode ).ToList();
            ViewBag.Discount = entities.OfferDetails.Where(e => e.ExpiryDate > DateTime.Now).Select(s =>  s.DiscountPercentage).ToList();

            return View(soapClient.GetRouteDetails(source, destination, doj));
        }
        public ActionResult SelectSeat(int id)
        {
            TempData["RouteId"] = id;
            var result = soapClient.GetRouteById(id).Single();
            TempData["busId"] = result.BusId;
            TempData.Keep();

            List<int> booked = soapClient.GetBookingStatuses(result.BusId).Where(s=>s.Status=="Booked").Select(b=>b.SeatNo).ToList();
            ViewBag.BookedSeat = booked;

            int source = result.SourceId;
            string sourceName = soapClient.GetCityDetails().Where(ci => ci.CityId.Equals(source)).Select(name => name.CityName).FirstOrDefault();
            ViewBag.source = sourceName;

            int destination = result.DestinationId;
            string destinationName = soapClient.GetCityDetails().Where(ci => ci.CityId.Equals(destination)).Select(name => name.CityName).FirstOrDefault();
            ViewBag.destination = destinationName;

            ViewBag.departure = Convert.ToDateTime(result.DepartureTime).ToString("hh:mm tt");
           // ViewBag.departure = departure;

            ViewBag.arrival = Convert.ToDateTime(result.ArrivalTime).ToString("hh:mm tt"); 
           // ViewBag.arrival = arrival;

            double price = result.Price;
            ViewBag.price = price;

            return View();
        }
        [HttpPost]
        public ActionResult SelectSeat(FormCollection collection)
        {
            ViewBag.arraySeat = collection["pp"];
            ViewBag.price = collection["qq"];
            string seatdata = collection["pp"];
            List<string> seatList = seatdata.Split(',').ToList();
            TempData["seats"] = seatList;
            TempData["price"] = collection["qq"];
            TempData.Keep();
            return View("Modal");
        }
        [HttpPost]
        public ActionResult Passenger(FormCollection collection)
        {
            ViewBag.price = TempData["price"];
            List<string> seatList = (List<string>)TempData["seats"];

            int routeId = Convert.ToInt32(TempData["RouteId"]);
            double price = Convert.ToDouble(ViewBag.price);
            int count = Convert.ToInt32(collection["count"]);
            soapClient.AddTicketDetails(routeId, count, price);
            int busId = Convert.ToInt32(TempData["busId"]);

            //string seatdata = ViewBag.arraySeat;
            //var seatList = seatdata.Split(',').ToList();
            //ViewBag.seatList = seatList;
        

            string name = collection["Name"].ToString();
            List<string> NameList = name.Split(',').ToList();


            string age = collection["Age"];
            List<string> AgeList = age.Split(',').ToList();

            int numberCount = seatList.Count + 2;
            List<string> gender = new List<string>();

            for (int i = 2; i < numberCount; i++)
            {
                gender.Add(collection[i]);
            }



            //ViewBag.Gender1 = collection["1"].ToString();
            //ViewBag.Gender2 = collection["2"].ToString();

            //List<string> Name = (List<string>)TempData["Name"];
            //List<string> Age = (List<string>)TempData["Age"];


            TempData.Keep();

            foreach (var item in seatList)
            {
                soapClient.BookTicket(Convert.ToInt32(item), busId);

            }
            //for (int i=1;i<=count;i++) {
            //    var result = soapClient.AddPassanger(collection["Name"], Convert.ToInt32(collection["Age"]), collection["Gender"], collection["Phone"],Convert.ToInt32(TempData["seat"]));
            //}
            int j = 0;
            foreach (var item in seatList)
            {
                soapClient.AddPassanger(NameList[j], Convert.ToInt32(AgeList[j]), gender[j], collection["Phone"], Convert.ToInt32(item));
                j++;
            }


            return View();
        }


        [HttpPost]
        public ActionResult Register(UserDetails user, FormCollection collection)
        {

            DateTime date = DateTime.Now;
            TempData["DateOfBirth"] = date.Year + "-0" + date.Month + "-0" + date.Day;

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
        public ActionResult Authorize(UserDetails login, FormCollection collection)
        {
            using (BusBookingSystemEntities entities = new BusBookingSystemEntities())
            {
                string num = login.MobileNumber;
                string pwd = login.Password;


                var userDetails = entities.UserDetails.Where(user => user.MobileNumber.Equals(num) && user.Password.Equals(pwd)).FirstOrDefault();
                //int loginid = entities.UserDetails.Where(s => s.MobileNumber == userDetails.MobileNumber).Select(s1 => s1.UserId).FirstOrDefault();
      

                if (userDetails == null)
                {
                    return View("Index");
                }
                else
                {
       
                    string role = userDetails.UserType;
                    if (role == "Admin")
                    {
                        Session["UserId"] = userDetails.UserId;
                        Session["Name"] = login.Name;
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (role == "User")
                    {
                        Session["UserId"] = userDetails.UserId;
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
        public ActionResult BusHire()
        {
            DateTime date = DateTime.Now;

            TempData["currentDate"] = date.Year + "-0" + date.Month + "-0" + date.Day;

            ViewBag.Source = new SelectList(entity.City, "CityId", "CityName");
            ViewBag.Destination = new SelectList(entity.City, "CityId", "CityName");
            return View();
        }

        [HttpPost]
        public ActionResult SaveDetails(FormCollection collection)
        {
            BookDetails book = new BookDetails();

            //book.BookId = b.BookId;
            book.Name = collection["Name"];
            book.Contact = Convert.ToInt64(collection["Contact"]);
            entity.BookDetails.Add(book);
            entity.SaveChanges();

            HireDetails hire = new HireDetails();

            hire.StartDate = Convert.ToDateTime(collection["StartDate"]).Date;
            hire.EndDate = Convert.ToDateTime(collection["EndDate"]).Date;
            hire.Type = collection["Type"];
            hire.Capacity = Convert.ToInt32(collection["Capacity"]);
            hire.Source = Convert.ToInt32(collection["Source"]);
            hire.Destination = Convert.ToInt32(collection["Destination"]);
            hire.Return = collection["Return"];
            hire.BookId = book.BookId;

            entity.BookDetails.Add(book);

            entity.HireDetails.Add(hire);
            entity.SaveChanges();

            return RedirectToAction("Success", "Home");
        }
        public ActionResult Success()
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
        [HttpPost]
        public JsonResult ApplyOffer(string key, string value)
        {
            var offerCode = value;
            var percent = entities.OfferDetails.Where(of => of.OfferCode == offerCode).Select(s => s.DiscountPercentage).Single();
            var price =Convert.ToInt32( TempData["price"]);
            ViewBag.newPrice =Convert.ToInt32( (price * percent) / 100);
            TempData.Keep();
            return this.Json(new {success = true });
        }
        public ActionResult TicketDetails()
        {

            return View();
        }
        public PartialViewResult GetMapView()
        {
            ViewBag.Source = TempData["source"];
            ViewBag.Destination = TempData["destination"];
            return PartialView("_GetMapView");
        }
        [HttpPost]
        public ActionResult CancelTicket(FormCollection collection)
        {
            soapClient.CancelTicket(Convert.ToInt32(collection["ticketNo"]));
            return RedirectToAction("Cancel");
        }
    }
}