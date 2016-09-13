using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MunicipalTrashProgram.Models;

namespace MunicipalTrashProgram.Controllers
{
    public class PersonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Person
        public ActionResult Index()
        {
            var people = db.people.Include(p => p.Address).Include(p => p.UserInfo).Include(p => p.Worker);
            return View(people.ToList());
            //List<Person> model = db.people.ToList();
            //return View(model);
            //return View(db.people.ToList());
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.people.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            ViewBag.Address_id = new SelectList(db.addresses, "Address_id", "Street");
            ViewBag.UserInfo_id = new SelectList(db.usersInfo, "UserInfo_id", "PickupDay");
            ViewBag.Worker_id = new SelectList(db.workers, "Worker_id", "Worker_id");
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Person_id,Address_id,Worker_id,UserInfo_id,FirstName,LastName,Password,_Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.people.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Address_id = new SelectList(db.addresses, "Address_id", "Street", person.Address_id);
            ViewBag.UserInfo_id = new SelectList(db.usersInfo, "UserInfo_id", "PickupDay", person.UserInfo_id);
            ViewBag.Worker_id = new SelectList(db.workers, "Worker_id", "Worker_id", person.Worker_id);
            return View(person);
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.people.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.Address_id = new SelectList(db.addresses, "Address_id", "Street", person.Address_id);
            ViewBag.UserInfo_id = new SelectList(db.usersInfo, "UserInfo_id", "PickupDay", person.UserInfo_id);
            ViewBag.Worker_id = new SelectList(db.workers, "Worker_id", "Worker_id", person.Worker_id);
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Person_id,Address_id,Worker_id,UserInfo_id,FirstName,LastName,Password,_Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Address_id = new SelectList(db.addresses, "Address_id", "Street", person.Address_id);
            ViewBag.UserInfo_id = new SelectList(db.usersInfo, "UserInfo_id", "PickupDay", person.UserInfo_id);
            ViewBag.Worker_id = new SelectList(db.workers, "Worker_id", "Worker_id", person.Worker_id);
            return View(person);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.people.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.people.Find(id);
            db.people.Remove(person);
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
