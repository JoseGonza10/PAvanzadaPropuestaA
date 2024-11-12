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
	public DbSet<Employee> Employee { get; set; }
	public DbSet<Material> Material { get; set; }
	public DbSet<Color> Color { get; set; }
	public DbSet<Vehicle> Vehicle { get; set; }
	public DbSet<Seat> Seat { get; set; }
	public DbSet<VehicleSeat> VehicleSeat { get; set; }
	public DbSet<LateralDesign> LateralDesign { get; set; }
	public DbSet<CentralDesign> CentralDesign { get; set; }
	public DbSet<Image> Image { get; set; }
	public DbSet<Design> Design { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Supplier> Supplier { get; set; }
	public DbSet<Product> Product { get; set; }
	public DbSet<Visit> Visit { get; set; }
	public DbSet<Invoice> Invoice { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=CUBREASIENTOS;Trusted_Connection=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
