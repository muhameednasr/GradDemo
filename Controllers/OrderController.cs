using GradDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ApplicationDbContext context;
        public OrderController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Order> orders = context.Orders.Include(u => u.Customer).Include(c=>c.Cashier).ToList();
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id:int}")] //api/order/1
        public IActionResult GetById(int id)
        {
            Order order = context.Orders.FirstOrDefault(o=>o.Id==id);
            return Ok(order);
        }
        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return RedirectToAction("GetById",order.Id);
        }

       
    }
}
