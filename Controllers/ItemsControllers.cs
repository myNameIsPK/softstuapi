using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftStuApi.Data;
using SoftStuApi.Models;

namespace SoftStuApi.Controllers
{
    [Route("api/[controller]")] // api/items
    [ApiController]
    public class ItemsController: ControllerBase
    {
        private readonly ApiDbContext _context;
        
        public ItemsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public IActionResult GetItems()
        {
            var items =  _context.Items.ToList();
            return Ok(items);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetItem(string name)
        {
            var items = await _context.Items.FirstOrDefaultAsync(z => z.Name == name);
            if(items == null)
                return NotFound();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemData data)
        {
            if(ModelState.IsValid)
            {
                await _context.Items.AddAsync(data);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetItem", new {data.Name}, data);
            }

            return new JsonResult("Somethign Went wrong") {StatusCode = 500};
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateItems(string name, ItemData item)
        {
            if(name != item.Name)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var existItem = await _context.Items.FirstOrDefaultAsync(z => z.Name == name);
                if(existItem == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }
    }
}