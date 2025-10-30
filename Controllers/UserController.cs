using GradDemo.DTOs.Users;
using GradDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSummaryDto>>> GetAll()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserSummaryDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role != null ? u.Role.Name : null
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("cashiers")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<IEnumerable<UserSummaryDto>>> GetCashiers()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role != null && u.Role.Name == "Cashier")
                .Select(u => new UserSummaryDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role != null ? u.Role.Name : null
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("captains")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<IEnumerable<UserSummaryDto>>> GetCaptains()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role != null && u.Role.Name == "Captain")
                .Select(u => new UserSummaryDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role != null ? u.Role.Name : null
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("waiters")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<IEnumerable<UserSummaryDto>>> GetWaiters()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role != null && u.Role.Name == "Waiter")
                .Select(u => new UserSummaryDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role != null ? u.Role.Name : null
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}