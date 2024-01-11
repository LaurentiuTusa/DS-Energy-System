using System;
using System.Collections.Generic;
using DAL.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public partial class UserDbContext : DbContext
{
    public UserDbContext()
    {
    }

    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("Data Source=DESKTOP-HMGHORR\\SQLEXPRESS;Initial Catalog=DS_User;Integrated Security=True;Trust Server Certificate=true");
       // => optionsBuilder.UseSqlServer("Server=10.132.74.70,1434; DataBase=DS_User;user=Lau;password=lau;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;");//lab ip address
       //=> optionsBuilder.UseSqlServer("Server=192.168.100.10,1434; DataBase=DS_User;user=Lau;password=lau;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;");//home ip address
       => optionsBuilder.UseSqlServer("Server=192.168.8.154,1434; DataBase=DS_User;user=Lau;password=lau;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;");//mobile data ip address

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
