using GradDemo.DTOs.Bills;
using GradDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireCashierRole")]
    public class BillController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BillController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillResponseDto>>> GetAll()
        {
            var bills = await _context.Bills
                .Include(b => b.Order)!
                    .ThenInclude(o => o!.OrderItems)!
                        .ThenInclude(oi => oi.Item)
                .Include(b => b.Cashier)
                .Select(b => new BillResponseDto
                {
                    Id = b.Id,
                    BillDate = b.BillDate,
                    PaymentMethod = b.PaymentMethod,
                    CashierName = b.Cashier != null ? b.Cashier.Name : string.Empty,
                    OrderId = b.OrderId,
                    Items = b.Order != null
                        ? b.Order.OrderItems.Select(oi => new BillItemDto
                        {
                            ItemName = oi.Item != null ? oi.Item.Name : string.Empty,
                            Quantity = oi.Quantity,
                            Price = oi.Item != null ? oi.Item.Price : 0
                        }).ToList()
                        : new List<BillItemDto>()
                })
                .ToListAsync();

            return Ok(bills);
        }

        [HttpPost]
        public async Task<ActionResult<BillResponseDto>> PlaceBill([FromBody] CreateBillRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)!
                    .ThenInclude(oi => oi.Item)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId);

            if (order == null)
            {
                return BadRequest("Invalid OrderId");
            }

            var cashierExists = await _context.Users.AnyAsync(u => u.Id == request.CashierId);
            if (!cashierExists)
            {
                return BadRequest("Invalid CashierId");
            }

            var bill = new Bill
            {
                OrderId = request.OrderId,
                CashierId = request.CashierId,
                PaymentMethod = request.PaymentMethod,
                BillDate = DateTime.UtcNow
            };

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            var response = new BillResponseDto
            {
                Id = bill.Id,
                BillDate = bill.BillDate,
                PaymentMethod = bill.PaymentMethod,
                CashierName = await _context.Users
                    .Where(u => u.Id == bill.CashierId)
                    .Select(u => u.Name)
                    .FirstOrDefaultAsync() ?? string.Empty,
                OrderId = order.Id,
                Items = order.OrderItems
                    .Select(oi => new BillItemDto
                    {
                        ItemName = oi.Item != null ? oi.Item.Name : string.Empty,
                        Quantity = oi.Quantity,
                        Price = oi.Item != null ? oi.Item.Price : 0
                    })
                    .ToList()
            };

            return CreatedAtAction(nameof(GetAll), new { id = bill.Id }, response);
        }
    }
}
