using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusReservation.Models;

namespace BusReservation.Controllers
{
    public class AdminController : Controller
    {
        BusBookingSystemEntities entities = new BusBookingSystemEntities();   
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AddProvider()
        {
            return PartialView("_AddProvider");
        }
        [HttpPost]
        public ActionResult AddProvider(ProviderDetails provider)
        {
            entities.ProviderDetails.Add(provider);
            entities.SaveChanges();
            return RedirectToAction("Index","Admin");
        }

        public ActionResult ProviderDetails()
        {
            List<ProviderDetails> provider=entities.ProviderDetails.ToList();
            return PartialView("_ProviderDetails",provider);
        }
        
       
        public ActionResult Edit(int id)
        {
            ProviderDetails provider = entities.ProviderDetails.Find(id);
            return PartialView("_Edit",provider);
        }
        [HttpPost]
        public ActionResult Edit(ProviderDetails provider)
        {
            entities.Entry(provider).State = EntityState.Modified;
            entities.SaveChanges();
            return RedirectToAction("Index","Admin");
        }
        public ActionResult ProviderDelete(int id)
        {
            ProviderDetails provider = entities.ProviderDetails.Find(id);
            entities.ProviderDetails.Remove(provider);
            entities.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        //Offer Operations

        public PartialViewResult AddOffer()
        {
            return PartialView("_AddOffer");
        }
        [HttpPost]
        public ActionResult AddOffer(OfferDetails offer)
        {
            entities.OfferDetails.Add(offer);
            entities.SaveChanges();
            return RedirectToAction("Index","Admin");
        }

        public ActionResult OfferDetails()
        {
            List<OfferDetails> offer = entities.OfferDetails.ToList();
            return PartialView("_OfferDetails", offer);
        }

        public ActionResult EditOffer(int id)
        {
            OfferDetails offer = entities.OfferDetails.Find(id);
            return PartialView("_EditOffer",offer);
        }

        [HttpPost]
        public ActionResult EditOffer(OfferDetails offer)
        {
            entities.Entry(offer).State = EntityState.Modified;
            entities.SaveChanges();
            return RedirectToAction("Index","Admin");
        }

        public ActionResult DeleteOffer(int id)
        {
            OfferDetails offer = entities.OfferDetails.Find(id);
            entities.OfferDetails.Remove(offer);
            entities.SaveChanges();
            return RedirectToAction("Index","Admin");
        }



    }
}