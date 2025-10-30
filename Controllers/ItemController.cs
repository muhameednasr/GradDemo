using GradDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.Items
                .Include(i => i.ItemSizes)!
                    .ThenInclude(s => s.Size)
                .Select(i => new
                {
                    i.Id,
                    i.Name,
                    i.Description,
                    i.Category,
                    i.Price,
                    Sizes = i.ItemSizes
                        .Select(s => new { s.Multiplier, SizeCode = s.Size != null ? s.Size.Code : string.Empty })
                        .ToList()
                })
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.Items
                .Include(i => i.ItemSizes)!
                    .ThenInclude(s => s.Size)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var item = await _context.Items
                .Include(i => i.ItemSizes)!
                    .ThenInclude(s => s.Size)
                .FirstOrDefaultAsync(i => i.Name == name);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("{itemId}/sizes")]
        public async Task<IActionResult> GetAvailableSizes(int itemId)
        {
            var sizes = await _context.ItemSize
                .Where(x => x.ItemId == itemId)
                .Include(x => x.Size)
                .Select(x => new { x.Size!.Id, x.Size.Code, x.Multiplier })
                .ToListAsync();

            return Ok(sizes);
        }
    }
}
