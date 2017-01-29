using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApplicationTest.Models;

namespace ApplicationTest.Controllers
{
    public class UserSysController : Controller
    {
        private StefaniniContext db = new StefaniniContext();

        // GET: UserSys
        public ActionResult Index()
        {
            var user = (UserSys)(Session["user"]);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.UserSys.ToList());
        }

        // GET: UserSys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSys userSys = db.UserSys.Find(id);
            if (userSys == null)
            {
                return HttpNotFound();
            }
            return View(userSys);
        }

        // GET: UserSys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserSys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Email,Password,UserRoleId")] UserSys userSys)
        {
            if (ModelState.IsValid)
            {
                db.UserSys.Add(userSys);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userSys);
        }

        // GET: UserSys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSys userSys = db.UserSys.Find(id);
            if (userSys == null)
            {
                return HttpNotFound();
            }
            return View(userSys);
        }

        // POST: UserSys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Email,Password,UserRoleId")] UserSys userSys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userSys);
        }

        // GET: UserSys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSys userSys = db.UserSys.Find(id);
            if (userSys == null)
            {
                return HttpNotFound();
            }
            return View(userSys);
        }

        // POST: UserSys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSys userSys = db.UserSys.Find(id);
            db.UserSys.Remove(userSys);
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
