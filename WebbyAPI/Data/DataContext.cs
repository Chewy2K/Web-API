using Microsoft.EntityFrameworkCore;
using WebbyAPI.Model;

namespace WebbyAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
    }
}
