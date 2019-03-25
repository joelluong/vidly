using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Vidly.Controllers.Api
{
    using AutoMapper;

    using Vidly.Dtos;
    using Vidly.Models;

    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // Get /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = this._context.Customers.
                Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            }

            var customerDto = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
            return this.Ok(customerDto);
            // delegate, reference to this method, we not call the method
        }


        // Get /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = this._context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return this.NotFound();
            }
            return this.Ok(Mapper.Map<Customer, CustomerDto>(customer));
            // we return only one customer, so we can not use the select extension of LINQ
        }


        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            // it created a new object and return it which we got here

            this._context.Customers.Add(customer);
            this._context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
            // so in web API is prefered use IHttpActionResult as the return type or the action
        }


        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var customerInDb = this._context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                return this.NotFound();
            }

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            // if we have an existing object, we can pass here as a second argument
            // this is because this object is loaded into our context
            
            // if want to learn more about Auto Mapper, read its documentation

            this._context.SaveChanges();

            return this.Ok();
        }


        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = this._context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                return this.NotFound();
            }

            this._context.Customers.Remove(customerInDb);
            this._context.SaveChanges();

            return this.Ok();
        }
    }
}
