using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftStuApi.Data;
using SoftStuApi.Models;

namespace SoftStuApi.Controllers
{
    [Route("api/[controller]")] // api/users
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiDbContext _context;
        
        public UsersController(ApiDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users =  _context.Users.ToList();
            return Ok(users);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(z => z.Id == id);
            if(user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserData data)
        {
            if(ModelState.IsValid)
            {
                await _context.Users.AddAsync(data);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new {data.Id}, data);
            }

            return new JsonResult("Somethign Went wrong") {StatusCode = 500};
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserData user)
        {
            if(id != user.Id)
                return BadRequest();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var existUser = await _context.Users.FirstOrDefaultAsync(z => z.Id == id);
                if(existUser == null)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(z => z.Id == id);

            if(existUser == null)
                return NotFound();

            _context.Users.Remove(existUser);
            await _context.SaveChangesAsync();

            return Ok(existUser);
        }
    }
}