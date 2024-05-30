using System;
using System.Collections.Generic;
using DiplomAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomAPI.Context;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Church> Churches { get; set; }

    public virtual DbSet<Localirytype> Localirytypes { get; set; }

    public virtual DbSet<Locality> Localities { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Photoofhram> Photoofhrams { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Regionhram> Regionhrams { get; set; }

    public virtual DbSet<Typeoflocality> Typeoflocalities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Church>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("church_pkey");

            entity.ToTable("church");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Builddate)
                .HasColumnType("character varying")
                .HasColumnName("builddate");
            entity.Property(e => e.BuilddateEng)
                .HasColumnType("character varying")
                .HasColumnName("builddate_eng");
            entity.Property(e => e.Churchname)
                .HasColumnType("character varying")
                .HasColumnName("churchname");
            entity.Property(e => e.ChurchnameEng)
                .HasColumnType("character varying")
                .HasColumnName("churchname_eng");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DescriptionEng).HasColumnName("description_eng");
            entity.Property(e => e.Idlocate).HasColumnName("idlocate");

            entity.HasOne(d => d.IdlocateNavigation).WithMany(p => p.Churches)
                .HasForeignKey(d => d.Idlocate)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("church_idlocate_fkey");
        });

        modelBuilder.Entity<Localirytype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("localirytype_pkey");

            entity.ToTable("localirytype");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idlocality).HasColumnName("idlocality");
            entity.Property(e => e.Idtypelocality).HasColumnName("idtypelocality");

            entity.HasOne(d => d.IdlocalityNavigation).WithMany(p => p.Localirytypes)
                .HasForeignKey(d => d.Idlocality)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("localirytype_idlocality_fkey");

            entity.HasOne(d => d.IdtypelocalityNavigation).WithMany(p => p.Localirytypes)
                .HasForeignKey(d => d.Idtypelocality)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("localirytype_idtypelocality_fkey");
        });

        modelBuilder.Entity<Locality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locality_pkey");

            entity.ToTable("locality");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nameoflocality)
                .HasColumnType("character varying")
                .HasColumnName("nameoflocality");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("photos_pkey");

            entity.ToTable("photos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Namephoto)
                .HasDefaultValueSql("'zaglushka.png'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("namephoto");
        });

        modelBuilder.Entity<Photoofhram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("photoofhram_pkey");

            entity.ToTable("photoofhram");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idchurch).HasColumnName("idchurch");
            entity.Property(e => e.Idphoto).HasColumnName("idphoto");

            entity.HasOne(d => d.IdchurchNavigation).WithMany(p => p.Photoofhrams)
                .HasForeignKey(d => d.Idchurch)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("photoofhram_idchurch_fkey");

            entity.HasOne(d => d.IdphotoNavigation).WithMany(p => p.Photoofhrams)
                .HasForeignKey(d => d.Idphoto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("photoofhram_idphoto_fkey");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("region_pkey");

            entity.ToTable("region");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nameofregion)
                .HasColumnType("character varying")
                .HasColumnName("nameofregion");
            entity.Property(e => e.NameofregionEng)
                .HasColumnType("character varying")
                .HasColumnName("nameofregion_eng");
            entity.Property(e => e.Regionphoto)
                .HasColumnType("character varying")
                .HasColumnName("regionphoto");
        });

        modelBuilder.Entity<Regionhram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("regionhram_pkey");

            entity.ToTable("regionhram");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idchurch).HasColumnName("idchurch");
            entity.Property(e => e.Idregion).HasColumnName("idregion");

            entity.HasOne(d => d.IdchurchNavigation).WithMany(p => p.Regionhrams)
                .HasForeignKey(d => d.Idchurch)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("regionhram_idchurch_fkey");

            entity.HasOne(d => d.IdregionNavigation).WithMany(p => p.Regionhrams)
                .HasForeignKey(d => d.Idregion)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("regionhram_idregion_fkey");
        });

        modelBuilder.Entity<Typeoflocality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("typeoflocality_pkey");

            entity.ToTable("typeoflocality");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Typelocality)
                .HasColumnType("character varying")
                .HasColumnName("typelocality");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
