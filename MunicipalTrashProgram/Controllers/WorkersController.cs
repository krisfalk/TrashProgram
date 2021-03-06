﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MunicipalTrashProgram.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Web.Profile;
using System.Web.UI;
using GoogleMaps.LocationServices;
using Newtonsoft.Json;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace MunicipalTrashProgram.Controllers
{
    public class WorkersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Workers
        public ActionResult Index()
        {
            ApplicationUser currentWorker = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<ApplicationUser> matchingZip = new List<ApplicationUser>();
            foreach (var user in db.Users)
            {
                if (user.Address_id != null)
                {
                    if (user.PickupDay == DateTime.Now.DayOfWeek.ToString())
                    {
                        if (currentWorker.Zip == user.Zip)
                        {
                            matchingZip.Add(user);
                        }
                    }
                }
            }
            List<ProgramAddress> latLng = new List<ProgramAddress>();
            for (int i = 0; i < matchingZip.Count; i++)
            {
                latLng.Add(GetLatAndLng(matchingZip[i]));
            }
            
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create("C:/Users/Kristofer/Documents/GitHub/data.xml", settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("markers");
            for (int i = 0; i < latLng.Count; i++)
            {
                double encodedXml = latLng[i].lat;
                double encodedXml2 = latLng[i].lng;
                writer.WriteStartElement("marker");
                writer.WriteAttributeString("lat", encodedXml.ToString());
                writer.WriteAttributeString("lng", encodedXml2.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();



            var model = new IndexViewModel
            {
                programmed = latLng,
                myUsers = matchingZip,
                currentUser = currentWorker,
                
            };
            return View(model);
        }
        private ProgramAddress GetLatAndLng(ApplicationUser user)
        {
            user.UserInfo = db.usersInfo.Where(x => x.UserInfo_id == user.UserInfo_id).SingleOrDefault();

            int houseNumber = db.addresses.Where(x => x.Address_id == user.Address_id).Select(x => x.HouseNumber).SingleOrDefault();
            string street = db.addresses.Where(x => x.Address_id == user.Address_id).Select(x => x.Street).SingleOrDefault();
            string city = db.addresses.Where(x => x.Address_id == user.Address_id).Select(x => x.City).SingleOrDefault();
            string state = db.addresses.Where(x => x.Address_id == user.Address_id).Select(x => x.State).SingleOrDefault();
            int zip = user.Zip;
            string country = "United States";
            string fullAddress = houseNumber.ToString() + " " + street + " " + city + ", " + country + " " + state + " " + zip;
            ProgramAddress mapAddress = new ProgramAddress();
            mapAddress.description = user.UserName;
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(fullAddress);
            mapAddress.lat = point.Latitude;
            mapAddress.lng = point.Longitude;
            mapAddress.description = user.UserName + "\r\n" + houseNumber + " " + street + ",\r\n" + city + ", " + state + " " + zip;

            
            return mapAddress;
        }

        // GET: Workers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            ApplicationUser currentWorker = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<ApplicationUser> matchingZip = new List<ApplicationUser>();
            foreach (var user in db.Users)
            {
                var entry = db.Entry(user);
                if (entry.Property(r => r.Address.Address_id).CurrentValue != null)
                {

                    if (currentWorker.Worker.WorkingZipCode == entry.Property(x => x.Address.ZipCode).CurrentValue)
                    {
                        matchingZip.Add(user);
                    }
                }
            }
            var model = new IndexViewModel
            {
                myUsers = matchingZip,
                currentUser = currentWorker
            };
            return View(model);
        }

        // GET: Workers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Worker_id,WorkingZipCode")] Worker worker, ApplicationUser user)
        {
            ApplicationUser myUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                db.workers.Add(worker);
                db.SaveChanges();
                using (var con = new ApplicationDbContext())
                {

                    myUser = con.Users.Find(myUser.Id);
                    myUser.Worker = worker;
                    myUser.Worker_id = worker.Worker_id;

                    con.Users.Attach(myUser);
                    var entry = con.Entry(myUser);
                    entry.Property(e => e.Worker_id).IsModified = true;
                    con.SaveChanges();
                }
                using (var con = new ApplicationDbContext())
                {
                    myUser = con.Users.Find(myUser.Id);
                    myUser.Zip = worker.WorkingZipCode;

                    con.Users.Attach(myUser);
                    var entry = con.Entry(myUser);
                    entry.Property(e => e.Zip).IsModified = true;
                    con.SaveChanges();

                }
                return RedirectToAction("Index", "Home");
            }

            return View(worker);
        }
        public ActionResult ViewAddresses()
        {
            return View();
        }

        // POST: Workers/View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewAddresses(Address address)
        {
            ApplicationUser currentWorker = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<ApplicationUser> matchingZip = new List<ApplicationUser>();
            foreach (var user in db.Users)
            {
                var entry = db.Entry(user);
                if (entry.Property(r => r.Address.Address_id).CurrentValue != null)
                {

                    if (currentWorker.Worker.WorkingZipCode == entry.Property(x => x.Address.ZipCode).CurrentValue)
                    {
                        matchingZip.Add(user);
                    }
                }
            }
            return View(matchingZip);
        }

        // GET: Workers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Worker_id,WorkingZipCode")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }
            return View(worker);
        }

        // GET: Workers/map
        public ActionResult map()
        {
            return View();
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Worker worker = db.workers.Find(id);
            db.workers.Remove(worker);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
