using System;
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

namespace MunicipalTrashProgram.Controllers
{
    public class UserInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private double costPerPickup = 9;

        // GET: UserInfoes
        public ActionResult Index()
        {
            DoWork doWork = new DoWork();
            ApplicationUser myUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            
            var firstOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var firstOfCurrentYear = new DateTime(DateTime.Now.Year, 1, 1);
            var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var currentDayOfWeek = ConvertDay(myUser.UserInfo.PickupDay);

            int modDate = 0;
            if (DateTime.Now < myUser.UserInfo.StartDate)
                modDate = myUser.UserInfo.VacationDays - CountDays(currentDayOfWeek, (DateTime)myUser.UserInfo.StartDate, (DateTime)myUser.UserInfo.EndDate);
            else if (DateTime.Now > myUser.UserInfo.StartDate && DateTime.Now < myUser.UserInfo.EndDate)
                modDate = myUser.UserInfo.VacationDays - CountDays(currentDayOfWeek, (DateTime)myUser.UserInfo.StartDate, DateTime.Now);
            else modDate = myUser.UserInfo.VacationDays;

            myUser.UserInfo.MonthlyBill = doWork.ComputeBill(CountDays(currentDayOfWeek, firstOfCurrentMonth, currentDateTime) - modDate, costPerPickup);
            myUser.UserInfo.YearlyBill = doWork.ComputeBill(CountDays(currentDayOfWeek, firstOfCurrentYear, currentDateTime) - modDate, costPerPickup);
            myUser.UserInfo.TotalBill = doWork.ComputeBill(CountDays(currentDayOfWeek, myUser.DateTime, currentDateTime) - modDate, costPerPickup);

            return View(db.usersInfo.ToList());
        }

        // GET: UserInfoes/Details/5
        public ActionResult Details(int? id)
        {
            DoWork doWork = new DoWork();
            ApplicationUser myUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.usersInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }

            var firstOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var firstOfCurrentYear = new DateTime(DateTime.Now.Year, 1, 1);
            var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var currentDayOfWeek = ConvertDay(myUser.UserInfo.PickupDay);

            userInfo.MonthlyBill = doWork.ComputeBill(CountDays(currentDayOfWeek, firstOfCurrentMonth, currentDateTime), costPerPickup);
            userInfo.YearlyBill = doWork.ComputeBill(CountDays(currentDayOfWeek, firstOfCurrentYear, currentDateTime), costPerPickup);
            userInfo.TotalBill = doWork.ComputeBill(CountDays(currentDayOfWeek, myUser.DateTime, currentDateTime), costPerPickup);

            return View(userInfo);
        }
        
        static int CountDays(DayOfWeek? day, DateTime start, DateTime end)
        {
            TimeSpan ts = end - start;                       // Total duration
            int count = (int)Math.Floor(ts.TotalDays / 7);   // Number of whole weeks
            int remainder = (int)(ts.TotalDays % 7);         // Number of remaining days
            int sinceLastDay = (int)(end.DayOfWeek - day);   // Number of days since last [day]
            if (sinceLastDay < 0) sinceLastDay += 7;         // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if (remainder >= sinceLastDay) count++;

            return count;
        }


        // GET: UserInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PickupDay")] UserInfo userInfo, ApplicationUser user)
        {
            ApplicationUser myUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                db.usersInfo.Add(userInfo);
                db.SaveChanges();
                
                userInfo.MonthlyBill = 0;
                userInfo.YearlyBill = 0;
                userInfo.TotalBill = 0;
                db.SaveChanges();

                using (var con = new ApplicationDbContext())
                {

                    myUser = con.Users.Find(myUser.Id);
                    myUser.UserInfo_id = userInfo.UserInfo_id;

                    con.Users.Attach(myUser);
                    var entry = con.Entry(myUser);
                    entry.Property(e => e.UserInfo_id).IsModified = true;
                    con.SaveChanges();
                }
                using (var con = new ApplicationDbContext())
                {
                    myUser = con.Users.Find(myUser.Id);
                    myUser.PickupDay = userInfo.PickupDay;

                    con.Users.Attach(myUser);
                    var entry = con.Entry(myUser);
                    entry.Property(e => e.PickupDay).IsModified = true;
                    con.SaveChanges();

                }
                return RedirectToAction("Index", "Home");
            }

            return View(userInfo);
        }

        // GET: UserInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.usersInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserInfo_id,PickupDay,MonthlyBill,YearlyBill,TotalBill")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.usersInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInfo userInfo = db.usersInfo.Find(id);
            db.usersInfo.Remove(userInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult VacationPeriod()
        {

            return View();
        }
        // POST: UserInfoes/VacationPeriod
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VacationPeriod([Bind(Include = "StartDate, EndDate")] UserInfo userInfo, ApplicationUser user)
        {
            using (var con = new ApplicationDbContext())
            {
                ApplicationUser myUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                var currentDayOfWeek = ConvertDay(myUser.UserInfo.PickupDay);
                myUser = con.Users.Find(myUser.Id);
                myUser.UserInfo.StartDate = userInfo.StartDate;
                myUser.UserInfo.EndDate = userInfo.EndDate;
                DateTime start;
                DateTime end;
                start = (DateTime)myUser.UserInfo.StartDate;
                end = (DateTime)myUser.UserInfo.EndDate;
                int num = CountDays(currentDayOfWeek, start, end);
                //userInfo.VacationDays = num;
                myUser.UserInfo.VacationDays = myUser.UserInfo.VacationDays + num;
                con.Users.Attach(myUser);
                var entry = con.Entry(myUser);
                entry.Property(e => e.UserInfo_id).IsModified = true;
                con.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
        private DayOfWeek ConvertDay(string myDay)
        {
            switch (myDay)
            {
                case "Monday":
                    return DayOfWeek.Monday;
                case "Tuesday":
                    return DayOfWeek.Tuesday;
                case "Wednesday":
                    return DayOfWeek.Tuesday;
                case "Thursday":
                    return DayOfWeek.Tuesday;
                case "Friday":
                    return DayOfWeek.Tuesday;
                case "Saturday":
                    return DayOfWeek.Tuesday;
                default:
                    return DayOfWeek.Monday;
            }
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
