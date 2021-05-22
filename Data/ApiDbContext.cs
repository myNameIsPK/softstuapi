using Microsoft.EntityFrameworkCore;
using SoftStuApi.Models;

namespace SoftStuApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<UserData> Users {get;set;}
        public DbSet<AdminData> Admins {get;set;}
        public DbSet<ItemData> Items {get;set;}
        public DbSet<BookingData> Bookings {get;set;}
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            
        }
    }
}