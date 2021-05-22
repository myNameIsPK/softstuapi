using Microsoft.EntityFrameworkCore;
using SoftStuApi.Models;

namespace SoftStuApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<UserData> Users {get;set;}
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            
        }
    }
}