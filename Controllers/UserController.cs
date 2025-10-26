using GradDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ApplicationDbContext context;
        public UserController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = context.Users.Include(u => u.Role).Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.Password,
                role = u.Role.Name
            }).ToList();
            return Ok(users);
        }

        [HttpGet("Cashier")]
        public IActionResult GetCashier()
        {
            var users = context.Users.Include(u => u.Role).Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.Password,
                role = u.Role.Name
            }).Where(u=>u.role=="Cashier").ToList();
            return Ok(users);
        }
        [HttpGet("Captain")]
        public IActionResult GetCaptain()
        {
            var users = context.Users.Include(u => u.Role).Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.Password,
                role = u.Role.Name
            }).Where(u => u.role == "Captain").ToList();
            return Ok(users);
        }
        [HttpGet("Waiter")]
        public IActionResult GetWaiter()
        {
            var users = context.Users.Include(u => u.Role).Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.Password,
                role = u.Role.Name
            }).Where(u => u.role == "Waiter").ToList();
            return Ok(users);
        }
    }
}