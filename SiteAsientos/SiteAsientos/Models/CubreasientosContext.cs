using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SiteAsientos.Models;

public partial class CubreasientosContext : DbContext
{

    public CubreasientosContext(DbContextOptions<CubreasientosContext> options)
        : base(options)
    {
    }

	// DbSets para cada entidad
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Material> Material { get; set; }
<<<<<<< Updated upstream
	public DbSet<Color> Colors { get; set; }
	public DbSet<Vehicle> Vehicles { get; set; }
	public DbSet<Seat> Seats { get; set; }
	public DbSet<VehicleSeat> VehicleSeats { get; set; }
	public DbSet<LateralDesign> LateralDesigns { get; set; }
	public DbSet<CentralDesign> CentralDesigns { get; set; }
=======
>>>>>>> Stashed changes
	public DbSet<Image> Images { get; set; }
	public DbSet<Design> Designs { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Supplier> Supplier { get; set; }
<<<<<<< Updated upstream
	public DbSet<Product> Products { get; set; }
	public DbSet<Visit> Visits { get; set; }
	public DbSet<Invoice> Invoices { get; set; }
=======
	public DbSet<Visit> Visit { get; set; }
	public DbSet<Invoice> Invoice { get; set; }
>>>>>>> Stashed changes

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Design
        modelBuilder.Entity<Design>().HasOne(x => x.Material).WithMany(x => x.Designs).HasForeignKey(f => f.Design_MaterialId);

        //Images
        modelBuilder.Entity<Image>().HasOne(x => x.Design).WithMany(x => x.Images).HasForeignKey(f => f.Image_DesignId);

        //Order
        modelBuilder.Entity<Order>().HasOne(x => x.Design).WithMany(x => x.Orders).HasForeignKey(f => f.Order_DesignId);

        //Material
        modelBuilder.Entity<Material>().HasOne(x => x.Supplier).WithMany(x => x.Materials).HasForeignKey(f => f.Material_SupplierId);

        //Visit
        modelBuilder.Entity<Visit>().HasOne(x => x.Employee).WithMany(x => x.Visits).HasForeignKey(f => f.Visit_EmployeeId);
        modelBuilder.Entity<Visit>().HasOne(x => x.Order).WithMany(x => x.Visits).HasForeignKey(f => f.Visit_OrderId);

        //Invoice
        modelBuilder.Entity<Invoice>().HasOne(x => x.Order).WithOne(x => x.Invoice).HasForeignKey<Invoice>(f => f.Invoice_OrderId);
    }

}
