using GradDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        ApplicationDbContext context;
        public ItemController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Item> items = context.Items.ToList();
            return Ok(items);
        }

        [HttpGet]
        [Route("{id:int}")] //api/order/1
        public IActionResult GetById(int id)
        {
            Item item = context.Items.FirstOrDefault(i => i.Id == id);
            return Ok(item);
        }
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Item item = context.Items.FirstOrDefault(i => i.Name == name);
            return Ok(item);
        }

        [HttpGet("{itemId}/sizes")]
        public IActionResult GetAvailableSizes(int itemId)
        {
            var sizes = context.ItemSize
                .Where(x => x.ItemId == itemId)
                .Include(x => x.Size)
                .Select(x => new { x.Size.Id, x.Size.Code })
                .ToList();

            return Ok(sizes);
        }

    }
}
