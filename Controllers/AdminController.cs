using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftStuApi.Data;
using SoftStuApi.Models;

namespace SoftStuApi.Controllers
{
    [Route("api/[controller]")] // api/admin
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApiDbContext _context;
        
        public AdminController(ApiDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public IActionResult GetAdmins()
        {
            var admins =  _context.Admins.ToList();
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(z => z.Id == id);
            if(admin == null)
                return NotFound();
            return Ok(admin);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(AdminData data)
        {
            if(ModelState.IsValid)
            {
                await _context.Admins.AddAsync(data);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetAdmin", new {data.Id}, data);
            }

            return new JsonResult("Somethign Went wrong") {StatusCode = 500};
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var existAdmin = await _context.Admins.FirstOrDefaultAsync(z => z.Id == id);

            if(existAdmin == null)
                return NotFound();

            _context.Admins.Remove(existAdmin);
            await _context.SaveChangesAsync();

            return Ok(existAdmin);
        }
    }
}