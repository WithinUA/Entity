using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity;

public partial class CountriesContext : DbContext
{
    public CountriesContext()
    {
    }

    public CountriesContext(DbContextOptions<CountriesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<SectionCustomer> SectionCustomers { get; set; }

    public virtual DbSet<Sell> Sells { get; set; }

    public virtual DbSet<WorldSide> WorldSides { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB;initial catalog=Countries;integrated security=True;MultipleActiveResultSets=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07BB6E176B");

            entity.ToTable("City");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__City__CountryId__4BAC3F29");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC070AF31A69");

            entity.ToTable("Country");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsFixedLength();

            entity.HasOne(d => d.WorldSide).WithMany(p => p.Countries)
                .HasForeignKey(d => d.WorldSideId)
                .HasConstraintName("FK__Country__WorldSi__5165187F");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0732D64935");

            entity.ToTable("Customer");

            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsFixedLength();

            entity.HasOne(d => d.Country).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__Countr__60A75C0F");

            entity.HasOne(d => d.Town).WithMany(p => p.Customers)
                .HasForeignKey(d => d.TownId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__TownId__619B8048");
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07E9FF7A16");

            entity.ToTable("Good");

            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Section).WithMany(p => p.Goods)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Good__SectionId__74AE54BC");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Section__3214EC079E69D1F1");

            entity.ToTable("Section");
        });

        modelBuilder.Entity<SectionCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SectionC__3214EC072A2BF156");

            entity.ToTable("SectionCustomer");

            entity.HasOne(d => d.Customer).WithMany(p => p.SectionCustomers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SectionCu__Custo__6D0D32F4");

            entity.HasOne(d => d.Section).WithMany(p => p.SectionCustomers)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SectionCu__Secti__6C190EBB");
        });

        modelBuilder.Entity<Sell>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC071C8A5659");

            entity.HasOne(d => d.Country).WithMany(p => p.Sells)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sells__CountryId__70DDC3D8");

            entity.HasOne(d => d.Good).WithMany(p => p.Sells)
                .HasForeignKey(d => d.GoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sells__GoodId__75A278F5");
        });

        modelBuilder.Entity<WorldSide>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07D7306048");

            entity.ToTable("WorldSide");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
