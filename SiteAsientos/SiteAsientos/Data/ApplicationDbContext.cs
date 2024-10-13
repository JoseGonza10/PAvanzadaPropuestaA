using Microsoft.EntityFrameworkCore;
using SiteAsientos.Models;

namespace SiteAsientos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSets para cada entidad
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<VehicleSeat> VehicleSeats { get; set; }
        public DbSet<LateralDesign> LateralDesigns { get; set; }
        public DbSet<CentralDesign> CentralDesigns { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
