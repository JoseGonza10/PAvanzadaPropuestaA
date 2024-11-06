using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SiteAsientos.Models;

public partial class CubreasientosContext : DbContext
{
    public CubreasientosContext()
    {
    }

    public CubreasientosContext(DbContextOptions<CubreasientosContext> options)
        : base(options)
    {
    }

	// DbSets para cada entidad
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Material> Material { get; set; }
	public DbSet<Color> Colors { get; set; }
	public DbSet<Vehicle> Vehicles { get; set; }
	public DbSet<Seat> Seats { get; set; }
	public DbSet<VehicleSeat> VehicleSeats { get; set; }
	public DbSet<LateralDesign> LateralDesigns { get; set; }
	public DbSet<CentralDesign> CentralDesigns { get; set; }
	public DbSet<Image> Images { get; set; }
	public DbSet<Design> Designs { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Supplier> Supplier { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Visit> Visits { get; set; }
	public DbSet<Invoice> Invoices { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=CUBREASIENTOS;Trusted_Connection=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
