using System;
using System.Collections.Generic;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Igrac> Igraci { get; set; }

    public virtual DbSet<Natjecanje> Natjecanja { get; set; }

    public virtual DbSet<Natjecatelj> Natjecatelji { get; set; }

    public virtual DbSet<Tim> Timovi { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Igrac>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("igrac_pkey");

            entity.ToTable("igrac");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DatumRodenja).HasColumnName("datum_rodenja");
            entity.Property(e => e.Ime)
                .HasMaxLength(50)
                .HasColumnName("ime");
            entity.Property(e => e.Prezime)
                .HasMaxLength(50)
                .HasColumnName("prezime");

            entity.HasOne(d => d.Natjecatelj).WithOne(p => p.Igrac)
                .HasForeignKey<Igrac>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("igrac_id_fkey");
        });

        modelBuilder.Entity<Natjecanje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("natjecanje_pkey");

            entity.ToTable("natjecanje");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Godina).HasColumnName("godina");
            entity.Property(e => e.MjestoFinale)
                .HasMaxLength(50)
                .HasColumnName("mjesto_finale");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("naziv");
            entity.Property(e => e.Organizator)
                .HasMaxLength(50)
                .HasColumnName("organizator");
            entity.Property(e => e.Prvak)
                .HasMaxLength(50)
                .HasColumnName("prvak");
            entity.Property(e => e.Sport)
                .HasMaxLength(50)
                .HasColumnName("sport");
        });

        modelBuilder.Entity<Natjecatelj>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("natjecatelj_pkey");

            entity.ToTable("natjecatelj");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Drzava)
                .HasMaxLength(50)
                .HasColumnName("drzava");
            entity.Property(e => e.Natjecanjeid).HasColumnName("natjecanjeid");
            entity.Property(e => e.Spol)
                .HasMaxLength(1)
                .HasColumnName("spol");

            entity.HasOne(d => d.Natjecanje).WithMany(p => p.Natjecatelji)
                .HasForeignKey(d => d.Natjecanjeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("natjecatelj_natjecanjeid_fkey");
        });

        modelBuilder.Entity<Tim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tim_pkey");

            entity.ToTable("tim");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("naziv");
            entity.Property(e => e.Osnovan).HasColumnName("osnovan");
            entity.Property(e => e.Trener)
                .HasMaxLength(50)
                .HasColumnName("trener");

            entity.HasOne(d => d.Natjecatelj).WithOne(p => p.Tim)
                .HasForeignKey<Tim>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tim_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
