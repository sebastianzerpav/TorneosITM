using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TorneosITM.Data.Models;

namespace TorneosITM.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdministradorItm> AdministradoresItm { get; set; }

    public virtual DbSet<Torneo> Torneos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdministradorItm>(entity =>
        {
            entity.HasKey(e => e.IdAministradorItm);

            entity.ToTable("AdministradorITM");

            entity.Property(e => e.IdAministradorItm)
                .ValueGeneratedNever()
                .HasColumnName("idAministradorITM");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Torneo>(entity =>
        {
            entity.HasKey(e => e.IdTorneos);

            entity.Property(e => e.IdTorneos).HasColumnName("idTorneos");
            entity.Property(e => e.IdAdministradorItm).HasColumnName("idAdministradorITM");
            entity.Property(e => e.Integrantes)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NombreEquipo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NombreTorneo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TipoTorneo)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAdministradorItmNavigation).WithMany(p => p.Torneos)
                .HasForeignKey(d => d.IdAdministradorItm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Torneos_AdministradorITM");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
