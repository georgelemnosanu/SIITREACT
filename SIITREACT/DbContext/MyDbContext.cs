using Microsoft.EntityFrameworkCore;
using SIITREACT.Service;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SIITREACT.Model
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
          
    }
        public DbSet<Appointment> Appointments { get; set; }
       
    }
}