using Microsoft.EntityFrameworkCore;
using HillarysHaircare.Models;

public class HillarysHaircareDbContext : DbContext
{
    public DbSet<Stylist> Stylists { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Service> Services { get; set; }

    public HillarysHaircareDbContext(DbContextOptions<HillarysHaircareDbContext> context) : base(context)
    {

    }
}