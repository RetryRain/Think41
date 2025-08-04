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

        // ✅ 1. Get all orders for a specific user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersForUser(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(new
            {
                userId = user.Id,
                userName = $"{user.First_Name} {user.Last_Name}",
                orders = user.Orders
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

        // ✅ 3. Get all orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

    }
}
