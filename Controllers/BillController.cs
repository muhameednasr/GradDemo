using GradDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {

        ApplicationDbContext context;
        public BillController(ApplicationDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var bill = context.Bills.Include(b => b.Order).Include(b => b.Cashier).Select(
                b=> new
                {
                    b.Id,
                    b.BillDate,
                    b.PaymentMethod,
                    b.Cashier.Name,
                    Items = b.Order.OrderItems.Select(oi => new
                    {
                        oi.Item.Name,
                        oi.Quantity,
                        oi.Item.Price
                    }).ToList()
                }
            );
                
            return Ok(bill);
        }
        [HttpPost]
        public IActionResult PlaceOrder(Bill bill)
        {

            if ( 
                !context.Users.Any(u => u.Id == bill.CashierId) ||    
                !context.Orders.Any(u => u.Id == bill.OrderId))
                return BadRequest("Invalid  CashierId or OrderId");

           
            context.Bills.Add(bill);
            context.SaveChanges();

            //return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
            return Ok(bill);
        }
    }
}
