using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    using Vidly.ViewModels;

    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            this._context = new ApplicationDbContext();
        }

        /// <summary>
        /// This object is disposable object,
        /// so we need to properly dispose this object
        /// so we override dispose method of this controller class
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            // we need to load related objects, eager loading, and need to import System.Data.Entity
            // var customers = this._context.Customers.Include(c => c.MembershipType).ToList();


            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = this._context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return this.HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = this._context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
                                {
                                    Customer = new Customer(),
                                    MembershipTypes = membershipTypes
                                };
            return this.View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                                    {
                                        MembershipTypes = this._context.MembershipTypes.ToList()
                                    };
                return this.View("CustomerForm", viewModel);
            }


            if (customer.Id == 0)
            {
                // new customer
                this._context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = this._context.Customers.Single(c => c.Id == customer.Id);
                
                // try update model method should not be use, not secured
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubcribedToNewletter = customer.IsSubcribedToNewletter;
            }
            this._context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = this._context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
                                {
                                    Customer = customer,
                                    MembershipTypes = this._context.MembershipTypes.ToList()
                                };

            return this.View("CustomerForm", viewModel);
        }
    }
}