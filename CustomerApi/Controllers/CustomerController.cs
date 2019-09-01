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
                _context.Customers.Add(new Customer {
                    FirstName = "Frank",
                    LastName = "Lampard",
                    DateOfBirth = Convert.ToDateTime("1978-06-20T00: 00:00")
                });
                _context.SaveChanges();
            }
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet]
        [Route("index")]
        public async Task<ActionResult<IEnumerable<Customer>>> FindCustomersAsync([FromQuery] string firstName , [FromQuery]string lastName)
        {
            var customers = from cust in _context.Customers
                         select cust;
            IQueryable<Customer> firstNameCust = null;
            IQueryable<Customer> lastNameCust = null;
;            if (!String.IsNullOrEmpty(firstName))
            {
                firstNameCust = customers.Where(s => s.FirstName.Contains(firstName));
            }

            if (!String.IsNullOrEmpty(lastName))
            {
                lastNameCust = customers.Where(s => s.LastName.Contains(lastName));
            }

            var firstNameList = await firstNameCust.ToListAsync();
            var lastNameList = await lastNameCust.ToListAsync();
            var concatList = firstNameList.Concat(lastNameList);

            if (!concatList.Any())
            {
                return NoContent();
            }

            return concatList.ToList();
        }


            // GET: api/customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerAsync(long id)
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
        public async Task<ActionResult<Customer>> AddCustomerAsync([FromBody]Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerAsync), new { id = customer.Id }, customer);
        }

        // PUT: api/customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(long id, [FromBody]Customer customer)
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
        public async Task<IActionResult> DeleteCustomerAsync(long id)
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
