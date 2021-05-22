using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftStuApi.Data;
using SoftStuApi.Models;

namespace SoftStuApi.Controllers
{
    [Route("api/[controller]")] // api/Booking
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApiDbContext _context;
        
        public BookingController(ApiDbContext context)
        {
            _context = context;
        }   

        [HttpGet]
        public IActionResult GetBookings()
        {
            var bookings =  _context.Bookings.ToList();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(z => z.Id == id);
            if(booking == null)
                return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingData data)
        {
            if(ModelState.IsValid)
            {
                await _context.Bookings.AddAsync(data);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBooking", new {data.Id}, data);
            }

            return new JsonResult("Somethign Went wrong") {StatusCode = 500};
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var existBooking = await _context.Bookings.FirstOrDefaultAsync(z => z.Id == id);

            if(existBooking == null)
                return NotFound();

            _context.Bookings.Remove(existBooking);
            await _context.SaveChangesAsync();

            return Ok(existBooking);
        }
    }
}