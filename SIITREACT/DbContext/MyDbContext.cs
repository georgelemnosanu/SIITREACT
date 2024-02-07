using Microsoft.EntityFrameworkCore;
using SIITREACT.Model;

    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
     public DbSet<City> City { get; set; }

}

