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
    public class CustomersController : Controller
    {
        private StefaniniContext db = new StefaniniContext();

        // GET: Customers
        public ActionResult Index(string sortOrder, string searchName, string searchCity, string searchClassification, string searchGender, string searchRegion, string searchSeller, string searchStartDate, string searchEndDate)
        {
            var user = (UserSys)(Session["user"]);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.searchCity = Helpers.ComboHelper.ComboCity(searchCity);
            ViewBag.searchClassification = Helpers.ComboHelper.ComboClassification(searchClassification);
            ViewBag.searchGender = Helpers.ComboHelper.ComboGender(searchGender);
            ViewBag.searchRegion = Helpers.ComboHelper.ComboRegion(searchRegion);
            ViewBag.searchSeller = Helpers.ComboHelper.ComboSeller(searchSeller);
            ViewBag.FilterName = searchName;

            var customers = from c in db.Customer
                            .Include(c => c.City)
                            .Include(c => c.Gender)
                            .Include(c => c.Classification)
                            .Include(c => c.Region)
                            .Include(c => c.User)
                            select c;


            #region Filtro Pesquisa

            CustomerFilter filter = new CustomerFilter();
            filter.Name = (!string.IsNullOrEmpty(searchName) ? searchName : string.Empty);
            filter.IdCity = (!string.IsNullOrEmpty(searchCity) ? Int32.Parse(searchCity) : 0);
            filter.IdClassification = (!string.IsNullOrEmpty(searchClassification) ? Int32.Parse(searchClassification) : 0);
            filter.IdGender = (!string.IsNullOrEmpty(searchGender) ? Int32.Parse(searchGender) : 0);
            filter.IdRegion = (!string.IsNullOrEmpty(searchRegion) ? Int32.Parse(searchRegion) : 0);
            filter.IdSeller = (!string.IsNullOrEmpty(searchSeller) ? Int32.Parse(searchSeller) : 0);

            filter.StartDate = (!string.IsNullOrEmpty(searchStartDate) ? DateTime.Parse(searchStartDate) : DateTime.Now);
            filter.EndDate = (!string.IsNullOrEmpty(searchEndDate) ? DateTime.Parse(searchEndDate) : DateTime.Now);

            if (!string.IsNullOrEmpty(searchName))
            {
                customers = customers.Where(c => c.Name.ToUpper().Contains(filter.Name.ToUpper()));
            }

            if (!string.IsNullOrEmpty(searchCity))
            {
                customers = customers.Where(c => c.City.Id.Equals(filter.IdCity));
            }

            if (!string.IsNullOrEmpty(searchClassification))
            {
                customers = customers.Where(c => c.Classification.Id.Equals(filter.IdClassification));
            }

            if (!string.IsNullOrEmpty(searchGender))
            {
                customers = customers.Where(c => c.Gender.Id.Equals(filter.IdGender));
            }

            if (!string.IsNullOrEmpty(searchRegion))
            {
                customers = customers.Where(c => c.Region.Id.Equals(filter.IdRegion));
            }

            if (user != null)
            {
                if (user.Login != "Administrator")
                {
                    if (!string.IsNullOrEmpty(user.Login))
                    {
                        customers = customers.Where(c => c.User.Id.Equals(user.Id));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(searchSeller))
                    {
                        customers = customers.Where(c => c.User.Id.Equals(filter.IdSeller));
                    }

                }

            }


            if (!string.IsNullOrEmpty(searchStartDate) && !string.IsNullOrEmpty(searchEndDate))
            {
                customers = customers.Where(c => c.LastPurchase >= filter.StartDate.Value && c.LastPurchase <= filter.EndDate.Value);
            }


            #endregion

            #region Ordenação

            switch (sortOrder)
            {
                case "Name_desc":
                    customers = customers.OrderByDescending(s => s.Name);
                    break;

                case "LastPurchase":
                    customers = customers.OrderBy(s => s.LastPurchase);
                    break;

                case "LastPurchase_desc":
                    customers = customers.OrderByDescending(s => s.LastPurchase);
                    break;

                case "Phone":
                    customers = customers.OrderBy(s => s.Phone);
                    break;

                case "Phone_desc":
                    customers = customers.OrderByDescending(s => s.Phone);
                    break;

                case "GenderId":
                    customers = customers.OrderBy(s => s.GenderId);
                    break;

                case "GenderId_desc":
                    customers = customers.OrderByDescending(s => s.GenderId);
                    break;

                case "CityId":
                    customers = customers.OrderBy(s => s.CityId);
                    break;

                case "CityId_desc":
                    customers = customers.OrderByDescending(s => s.CityId);
                    break;

                case "RegionId":
                    customers = customers.OrderBy(s => s.RegionId);
                    break;

                case "RegionId_desc":
                    customers = customers.OrderByDescending(s => s.RegionId);
                    break;

                case "ClassificationId":
                    customers = customers.OrderBy(s => s.ClassificationId);
                    break;

                case "ClassificationId_desc":
                    customers = customers.OrderByDescending(s => s.ClassificationId);
                    break;

                case "UserId":
                    customers = customers.OrderBy(s => s.UserId);
                    break;

                case "UserId_desc":
                    customers = customers.OrderByDescending(s => s.UserId);
                    break;

                default:
                    customers.OrderBy(s => s.Name);
                    break;
            }

            #endregion

            return View(customers.ToList());
            
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,GenderId,CityId,RegionId,LastPurchase,ClassificationId,UserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,GenderId,CityId,RegionId,LastPurchase,ClassificationId,UserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
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
