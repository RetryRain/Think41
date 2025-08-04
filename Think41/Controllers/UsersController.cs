using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Think41.Data;

namespace Think41.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/users — Get all users with basic info + order count
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users
                .Include(u => u.Orders)
                .Select(u => new
                {
                    u.Id,
                    FullName = u.First_Name + " " + u.Last_Name,
                    u.Email,
                    u.Age,
                    u.City,
                    u.Country,
                    OrderCount = u.Orders.Count
                })
                .ToListAsync();

            return Ok(users);
        }

        // ✅ GET: api/users/{id} — Get user details + order count
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(new
            {
                user.Id,
                FullName = user.First_Name + " " + user.Last_Name,
                user.Email,
                user.City,
                user.Country,
                OrderCount = user.Orders?.Count ?? 0
            });
        }
    }
}
