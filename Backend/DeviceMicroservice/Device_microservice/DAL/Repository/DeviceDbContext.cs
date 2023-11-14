using System;
using System.Collections.Generic;
using DAL.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public partial class DeviceDbContext : DbContext
{
    public DeviceDbContext()
    {
    }

    public DeviceDbContext(DbContextOptions<DeviceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("Data Source=DESKTOP-HMGHORR\\SQLEXPRESS;Initial Catalog=DS_Device;Integrated Security=True;Trust Server Certificate=true");
       => optionsBuilder.UseSqlServer("Server=192.168.100.10,1434; DataBase=DS_Device;user=Lau;password=lau;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Devices).HasConstraintName("FK_Device_Device");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
