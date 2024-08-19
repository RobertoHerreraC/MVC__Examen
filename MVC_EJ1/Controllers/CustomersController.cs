using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using MVC_EJ1.Models;

namespace MVC_EJ1.Controllers
{
    public class CustomersController : Controller
    {
        private CodigoDBEntities db = new CodigoDBEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.Where(x => x.IsActive).ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Where(x => x.CustomerID == id && x.IsActive).FirstOrDefault();
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Name,DocumentNumber,DocumentType")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                customers.IsActive = true;
                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customers);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Where(x => x.CustomerID == id && x.IsActive).FirstOrDefault();
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Name,DocumentNumber,DocumentType")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                var customerObj = db.Customers.Where(x => x.CustomerID == customers.CustomerID).FirstOrDefault();
                db.Entry(customerObj).State = EntityState.Modified;
                customerObj.Name = customers.Name;
                customerObj.DocumentNumber = customers.DocumentNumber;
                customerObj.DocumentType = customers.DocumentType;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customers);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Where(x => x.CustomerID == id && x.IsActive).FirstOrDefault();
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customers = db.Customers.Where(x => x.CustomerID == id && x.IsActive).FirstOrDefault();
            if (customers == null)
            {
                return HttpNotFound();
            }
            db.Entry(customers).State = EntityState.Modified;
            customers.IsActive = false;
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

        public ActionResult InactiveCustomers()
        {
            return View(db.Customers.Where(x => x.IsActive == false).ToList());
        }
    }
}
