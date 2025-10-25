using GradDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GradDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public OrderController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Cashier)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                .Include(o=>o.Captain)
                .Include(o=>o.Waiter)
                .Include(o=>o.Table)
                .Select(o => new
                {
                    o.Id,
                    o.OrderDate,
                    CustomerName = o.Customer.Name,
                    CashierName = o.Cashier.Name,
                    WaiterName = o.Waiter.Name,
                    CaptainName = o.Captain.Name,
                    o.TableId,
                    o.Table.Area,
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
        //api/order/{id}  ------> dont start with slash '/' ya zmely  ;)
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var order = context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Cashier)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                .Include(o => o.Captain)
                .Include(o => o.Waiter)
                .Include(o => o.Table)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            
            if (!context.Users.Any(u => u.Id == order.CustomerId) ||
                !context.Users.Any(u => u.Id == order.CashierId)||
                !context.Users.Any(u => u.Id == order.CaptainId)||
                !context.Users.Any(u => u.Id == order.WaiterId))
                return BadRequest("Invalid CustomerId or CashierId or CaptainId or WaiterId");

            foreach (var oi in order.OrderItems)
            {
                var item = context.Items.Find(oi.ItemId);
                if (item == null)
                    return BadRequest($"Item with ID {oi.ItemId} not found.");

                oi.Item = item;  
            }

            order.CalculateTotal();
            order.OrderDate = DateTime.Now;

            context.Orders.Add(order);
            context.SaveChanges();

            //return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
            return Ok(order);
        }

        //api/order/{id}  ------> dont start with slash '/' ya zmely  ;)
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            var oldOrder = context.Orders
                .Include(o => o.OrderItems)  
                .FirstOrDefault(o => o.Id == id);

            if (oldOrder == null)
                return NotFound($"Order with ID {id} not found");

            if (!context.Users.Any(u => u.Id == order.CustomerId) ||
             !context.Users.Any(u => u.Id == order.CashierId) ||
             !context.Users.Any(u => u.Id == order.CaptainId) ||
             !context.Users.Any(u => u.Id == order.WaiterId))
                return BadRequest("Invalid CustomerId or CashierId or CaptainId or WaiterId");


            foreach (var oi in order.OrderItems)
            {
                var item = context.Items.Find(oi.ItemId);
                if (item == null)
                    return BadRequest($"Item with ID {oi.ItemId} not found.");
                oi.Item = item;
            }

            
            oldOrder.Status = order.Status;
            oldOrder.CustomerId = order.CustomerId;
            oldOrder.CashierId = order.CashierId;
            oldOrder.CaptainId = order.CaptainId;
            oldOrder.WaiterId = order.WaiterId;
            oldOrder.TableId = order.TableId;
            

           
            context.OrderItems.RemoveRange(oldOrder.OrderItems);

           
            oldOrder.OrderItems = order.OrderItems;

           
            oldOrder.CalculateTotal();

            context.SaveChanges();

            return Ok(oldOrder);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = context.Orders.Find(id);

            if (order == null)
                return NotFound($"Order with ID {id} not found");

            context.Orders.Remove(order);
            context.SaveChanges();

            return NoContent(); // 204 
        }
    }
}
/*  Test Object
 {
  "status": "test",
  "customerId": 1,
  "cashierId": 2,
  "orderItems": [
    { "quantity": 4, "itemId": 1 },
    { "quantity": 5, "itemId": 2 }
  ]
}
*/