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
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Size)
                .Include(o => o.Captain)
                .Include(o => o.Waiter)
                .Include(o => o.Table)
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
                        oi.Item.Price,
                        oi.Size.Code
                    }).ToList()
                })
                .ToList();


            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var order = context.Orders
                     .Include(o => o.Customer)
                     .Include(o => o.Cashier)
                     .Include(o => o.OrderItems)
                         .ThenInclude(oi => oi.Item)
                     .Include(o => o.OrderItems)
                         .ThenInclude(oi => oi.Size)
                     .Include(o => o.Captain)
                     .Include(o => o.Waiter)
                     .Include(o => o.Table)
                     .Where(o => o.Id == id)
                     .Select(o => new
                     {
                         o.Id,
                         o.OrderDate,
                         CustomerName = o.Customer.Name,
                         CashierName = o.Cashier.Name,
                         WaiterName = o.Waiter.Name,
                         CaptainName = o.Captain.Name,
                         o.Table.Area,
                         o.Status,
                         o.Total,
                         Items = o.OrderItems.Select(oi => new
                         {
                             ItemName = oi.Item.Name,
                             oi.Quantity,
                             oi.Item.Price,
                             SizeCode = oi.Size.Code
                         })
                     })
                    .FirstOrDefault();

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("today")]
        public IActionResult GetTodayOrders(int pageNumber = 1, int pageSize = 10)
        {
            var today = DateTime.Today;
            var startOfDay = today;
            var endOfDay = today.AddDays(1);

            var query = context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .Where(o => o.OrderDate >= startOfDay && o.OrderDate < endOfDay);

            var totalCount = query.Count();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (pageNumber > totalPages && totalPages > 0)
                return BadRequest($"Page number is more than total pages ({totalPages}).");

            var orders = query
                .OrderByDescending(o => o.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalRevenue = query.Sum(o => (decimal?)o.Total) ?? 0;
            var paidOrdersCount = query.Count(o => o.Status == "Paid");
            var unPaidOrdersCount = query.Count(o => o.Status != "Paid");

            var result = new
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalOrders = totalCount,
                TotalRevenue = totalRevenue,
                PaidOrders = paidOrdersCount,
                UnPaidOrders = unPaidOrdersCount,
                Orders = orders
            };

            return Ok(result);
        }


        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            try
            {
                
                if (order == null)
                {
                    return BadRequest("Order cannot be null");
                }

                
                if (order.OrderItems == null)
                {
                    order.OrderItems = new List<OrderItem>();
                }

                if (!order.OrderItems.Any())
                {
                    return BadRequest("Order must contain at least one item");
                }


                if (!context.Users.Any(u => u.Id == order.CustomerId) ||
                    !context.Users.Any(u => u.Id == order.CashierId) ||
                    !context.Users.Any(u => u.Id == order.CaptainId) ||
                    !context.Users.Any(u => u.Id == order.WaiterId))
                {
                    return BadRequest(new { message = "Invalid user IDs", data = order });
                }

                // Validate and load items with their sizes
                foreach (var oi in order.OrderItems)
                {
                    // Validate SizeId
                    if (oi.SizeId <= 0)
                    {
                        return BadRequest($"Invalid SizeId for item {oi.ItemId}");
                    }

                    // Load the item with ItemSizes
                    var item = context.Items
                        .Include(i => i.ItemSizes)
                        .FirstOrDefault(i => i.Id == oi.ItemId);

                    if (item == null)
                    {
                        return BadRequest($"Item with ID {oi.ItemId} not found.");
                    }

                    
                    var itemSize = item.ItemSizes.FirstOrDefault(its => its.SizeId == oi.SizeId);
                    if (itemSize == null)
                    {
                        return BadRequest($"Size with ID {oi.SizeId} not available for item {item.Name}");
                    }

                    // Attach the item to orderItem
                    oi.Item = item;
                }

                
                order.CalculateTotal();
                order.OrderDate = DateTime.Now;


                context.Orders.Add(order);
                context.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {

            using var transaction = context.Database.BeginTransaction();

            try
            {

                var oldOrder = context.Orders
                             .Include(o => o.OrderItems)
                                .ThenInclude(oi => oi.Item)
                                    .ThenInclude(i => i.ItemSizes)
                             .FirstOrDefault(o => o.Id == id);

                if (oldOrder == null)
                    return NotFound($"Order with ID {id} not found");

                if (!context.Users.Any(u => u.Id == order.CustomerId) ||
                 !context.Users.Any(u => u.Id == order.CashierId) ||
                 !context.Users.Any(u => u.Id == order.CaptainId) ||
                 !context.Users.Any(u => u.Id == order.WaiterId))
                    return BadRequest("Invalid CustomerId or CashierId or CaptainId or WaiterId");

                //here if we want to pay item or more from order seperatly
                Order newOrder = new Order() { OrderItems = new List<OrderItem>() };

                foreach (var oi in order.OrderItems.Where(x => x.IsPayed))
                {
                    // Load item with sizes for calculation
                    var item = context.Items
                        .Include(i => i.ItemSizes)
                        .FirstOrDefault(i => i.Id == oi.ItemId);

                    var paidItem = new OrderItem
                    {
                        ItemId = oi.ItemId,
                        Quantity = oi.Quantity,
                        SizeId = oi.SizeId,
                        IsPayed = true,
                        Item = item
                    };
                    newOrder.OrderItems.Add(paidItem);
                }
                newOrder.Status = "Paid";
                newOrder.CustomerId = order.CustomerId;
                newOrder.CashierId = order.CashierId;
                newOrder.CaptainId = order.CaptainId;
                newOrder.WaiterId = order.WaiterId;
                newOrder.TableId = order.TableId;
                newOrder.CalculateTotal();

                context.Orders.Add(newOrder);
                context.SaveChanges();

                Bill newBill = new Bill();
                newBill.OrderId = newOrder.Id;
                newBill.CashierId = newOrder.CashierId;

                context.Bills.Add(newBill);
                context.SaveChanges();

                // Update old order items
                var unpaidItems = new List<OrderItem>();
                foreach (var oi in order.OrderItems.Where(x => !x.IsPayed))
                {
                    var item = context.Items
                        .Include(i => i.ItemSizes)
                        .FirstOrDefault(i => i.Id == oi.ItemId);

                    unpaidItems.Add(new OrderItem
                    {
                        ItemId = oi.ItemId,
                        Quantity = oi.Quantity,
                        SizeId = oi.SizeId,
                        IsPayed = false,
                        Item = item
                    });
                }

                oldOrder.OrderItems = unpaidItems;
                oldOrder.CalculateTotal();

                oldOrder.Status = "Pending";
                oldOrder.CustomerId = order.CustomerId;
                oldOrder.CashierId = order.CashierId;
                oldOrder.CaptainId = order.CaptainId;
                oldOrder.WaiterId = order.WaiterId;
                oldOrder.TableId = order.TableId;

                context.SaveChanges();
                transaction.Commit();

                return Ok(oldOrder);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult CancelOrder(int id,[FromQuery]  string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                return BadRequest("Cancellation reason is required");

            var order = context.Orders.Find(id);

            if (order == null)
                return NotFound($"Order with ID {id} not found");

            CancelledOrder cancelledOrder = new CancelledOrder();
            cancelledOrder.OrderId = order.Id;
            cancelledOrder.CancelledById = order.CashierId;
            cancelledOrder.CancelledDate = DateTime.Now;
            cancelledOrder.Reason = reason;
            context.CancelledOrders.Add(cancelledOrder);

            //updating cancelled order status
            order.Status = "Cancelled";
            context.SaveChanges();

            return NoContent(); // 204 
        }
        [HttpGet("Cancelled")]
        public IActionResult GetCancelledOrders() {
            var cancelledOrders = context.CancelledOrders
                                 .Include(o=>o.Order)
                                    .ThenInclude(oi=>oi.Waiter)
                                 .Include(o=>o.Order)
                                    .ThenInclude(oi=>oi.OrderItems)
                                        .ThenInclude(i=>i.Item)
                                 .Include(o => o.Order)
                                    .ThenInclude(oi => oi.OrderItems)
                                        .ThenInclude(i => i.Size)
                                 .Include(o=>o.CancelledBy).ToList();
            return Ok(cancelledOrders);
        }
    }
}