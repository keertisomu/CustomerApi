using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly Models.CustomerContext _context;
        public CustomerController(CustomerContext context)
        {
            _context = context;

            if (_context.Customers.Count() == 0)
            {
                // Create a new customer if collection is empty,
                // which means you can't delete all customers.
                _context.Customers.Add(new Customer { FirstName = "Lampard" });
                _context.SaveChanges();
            }
        }


        //public async Task<ActionResult<IEnumerable<Customer>>> Index(string searchString)
        //{

        //    return await _context.Customers.Where(c => c.FirstName.Contains(searchString) || c.LastName.Contains(searchString)).ToListAsync();

        //    //var customers = from cust in _context.Customers
        //    //             select cust;

        //    //if (!String.IsNullOrEmpty(searchString))
        //    //{
        //    //    customers = customers.Where(c => c.FirstName.Contains(searchString) || c.LastName.Contains(searchString));
        //    //}

        //    //return await customers
        //}


        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            //if(searchTerm == null)
            //{
            //    return await _context.Customers.Where(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm)).ToListAsync();
            //}
            //{
                return await _context.Customers.ToListAsync();
            //}
        }

        [HttpGet]
        [Route("index")]
        public async Task<ActionResult<IEnumerable<Customer>>> FindCustomers([FromQuery(Name = "firstorlast")] string name)
        {
            var customers = from cust in _context.Customers
                         select cust;
            if (!String.IsNullOrEmpty(name))
            {
                customers = customers.Where(s => s.FirstName.Contains(name));
            }

            if(customers == null)
            {
                return NoContent();
            }

            return (await customers.ToListAsync());
        }


            // GET: api/customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody]Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT: api/customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, [FromBody]Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
