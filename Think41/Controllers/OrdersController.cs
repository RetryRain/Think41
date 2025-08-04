using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Think41.Data;

namespace Think41.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 1. Get all orders for a specific customer
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersForCustomer(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            return Ok(new
            {
                customerId = customer.Id,
                customerName = $"{customer.First_Name} {customer.Last_Name}",
                orders = customer.Orders
            });
        }

        // ✅ 2. Get specific order details
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Order_Id == orderId);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            return Ok(order);
        }
    }
}
