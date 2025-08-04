using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Think41.Data;

namespace Think41.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers
                .Select(c => new {
                    c.Id,
                    FullName = c.First_Name + " " + c.Last_Name,
                    c.Email,
                    c.Age,
                    c.City,
                    c.Country
                }).ToListAsync();

            return Ok(customers);
        }

        // GET: api/customers/457
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            return Ok(new
            {
                customer.Id,
                FullName = customer.First_Name + " " + customer.Last_Name,
                customer.Email,
                customer.City,
                customer.Country,
                OrderCount = customer.Orders?.Count ?? 0
            });
        }
    }

}
