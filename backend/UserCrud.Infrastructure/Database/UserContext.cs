namespace UserCrud.Infrastructure.Database
{
    using Microsoft.EntityFrameworkCore;
    using UserCrud.Domain.Entities;

    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
