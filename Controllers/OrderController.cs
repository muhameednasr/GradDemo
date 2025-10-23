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
            var orders = context.Orders
                 .Select(o => new
                 {
                     o.Id,
                     o.OrderDate,
                     CustomerName = o.Customer.Name,
                     CashierName = o.Cashier.Name,
                     o.Status,
                     o.Total,
                     Items = o.OrderItems.Select(oi => new
                     {
                         oi.Item.Name,
                         oi.Quantity,
                         oi.Item.Price
                     }).ToList()
                 })
                     .ToList();


            return Ok(orders);
        }

        [HttpGet]
        [Route("{id:int}")] //api/order/1
        public IActionResult GetById(int id)
        {
            Order order = context.Orders.FirstOrDefault(o => o.Id == id);
            return Ok(order);
        }
        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return CreatedAtAction("GetById", new { id = order.Id });
        }


    }
}
