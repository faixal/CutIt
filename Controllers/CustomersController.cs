using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CutIt.DataAccess;

namespace CutIt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private CutItEntities db = new CutItEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Admin_Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Customers admin_Customers = db.Admin_Customers.Find(id);
            if (admin_Customers == null)
            {
                return HttpNotFound();
            }
            return View(admin_Customers);
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
        public ActionResult Create([Bind(Include = "CustomerId,Name,Mobile,DateOfBirth,FirstVisitDate")] Admin_Customers admin_Customers)
        {
            if (ModelState.IsValid)
            {
                admin_Customers.CreatedBy = admin_Customers.UpdatedBy = User.Identity.Name;
                admin_Customers.CreatedDate = admin_Customers.UpdatedDate = DateTime.UtcNow;

                db.Admin_Customers.Add(admin_Customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_Customers);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Customers admin_Customers = db.Admin_Customers.Find(id);
            if (admin_Customers == null)
            {
                return HttpNotFound();
            }
            return View(admin_Customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Name,Mobile,DateOfBirth,FirstVisitDate")] Admin_Customers admin_Customers)
        {
            if (ModelState.IsValid)
            {
                Admin_Customers obj = db.Admin_Customers.Find(admin_Customers.CustomerId);
                if (obj == null)
                {
                    return HttpNotFound();
                }

                obj.Name = admin_Customers.Name;
                obj.Mobile = admin_Customers.Mobile;
                obj.DateOfBirth = admin_Customers.DateOfBirth;
                obj.FirstVisitDate = admin_Customers.FirstVisitDate;
                obj.UpdatedBy = User.Identity.Name;
                obj.UpdatedDate = DateTime.UtcNow;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_Customers);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Customers admin_Customers = db.Admin_Customers.Find(id);
            if (admin_Customers == null)
            {
                return HttpNotFound();
            }
            return View(admin_Customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Admin_Customers admin_Customers = db.Admin_Customers.Find(id);
            db.Admin_Customers.Remove(admin_Customers);
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
