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
    [Authorize(Roles ="Admin")]
    public class ServicesController : Controller
    {
        private CutItEntities db = new CutItEntities();

        // GET: Services
        public ActionResult Index()
        {
            return View(db.Admin_Services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Services admin_Services = db.Admin_Services.Find(id);
            if (admin_Services == null)
            {
                return HttpNotFound();
            }
            return View(admin_Services);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,Name,Charge")] Admin_Services admin_Services)
        {
            if (ModelState.IsValid)
            {
                admin_Services.CreatedBy = User.Identity.Name;
                admin_Services.CreatedDate = DateTime.UtcNow;
                admin_Services.UpdatedBy = User.Identity.Name;
                admin_Services.UpdatedDate = DateTime.UtcNow;

                db.Admin_Services.Add(admin_Services);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_Services);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Services admin_Services = db.Admin_Services.Find(id);
            if (admin_Services == null)
            {
                return HttpNotFound();
            }
            return View(admin_Services);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,Name,Charge")] Admin_Services admin_Services)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Admin_Services.Find(admin_Services.ServiceId);
                if (obj == null)
                {
                    return HttpNotFound();
                }

                obj.Name = admin_Services.Name;
                obj.Charge = admin_Services.Charge;

                obj.UpdatedBy = User.Identity.Name;
                obj.UpdatedDate = DateTime.UtcNow;

                //db.Entry(admin_Services).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_Services);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Services admin_Services = db.Admin_Services.Find(id);
            if (admin_Services == null)
            {
                return HttpNotFound();
            }
            return View(admin_Services);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin_Services admin_Services = db.Admin_Services.Find(id);
            db.Admin_Services.Remove(admin_Services);
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
