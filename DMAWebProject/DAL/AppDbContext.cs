using DMAWebProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DMAWebProject.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Products>  Products { get; set; }
        //test

    }
}
