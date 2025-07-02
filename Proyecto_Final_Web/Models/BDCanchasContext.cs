using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final_Web.Models;

public partial class BDCanchasContext : DbContext
{
    public BDCanchasContext()
    {
    }

    public BDCanchasContext(DbContextOptions<BDCanchasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cancha> Canchas { get; set; }

    public virtual DbSet<EstadosFactura> EstadosFacturas { get; set; }

    public virtual DbSet<EstadosReserva> EstadosReservas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<MetodosPago> MetodosPagos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tarifa> Tarifas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\BDCanchas.mdf; Integrated Security=True");
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cancha>(entity =>
        {
            entity.HasKey(e => e.IdCancha).HasName("PK__tmp_ms_x__BE05011165EF37B8");

            entity.HasIndex(e => e.Nombre, "UQ__tmp_ms_x__75E3EFCFF7776240").IsUnique();

            entity.Property(e => e.Activa).HasDefaultValue(true);
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.TipoCancha).HasMaxLength(50);
            entity.Property(e => e.Ubicacion).HasMaxLength(255);
        });

        modelBuilder.Entity<EstadosFactura>(entity =>
        {
            entity.HasKey(e => e.IdEstadoFactura).HasName("PK__EstadosF__D1BC26E796E2A879");

            entity.ToTable("EstadosFactura");

            entity.HasIndex(e => e.Nombre, "UQ__EstadosF__75E3EFCFA741CF14").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<EstadosReserva>(entity =>
        {
            entity.HasKey(e => e.IdEstadoReserva).HasName("PK__EstadosR__3E654CA8A0597987");

            entity.ToTable("EstadosReserva");

            entity.HasIndex(e => e.Nombre, "UQ__EstadosR__75E3EFCF9E40AB46").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Facturas__50E7BAF1416C5A38");

            entity.HasIndex(e => e.IdReserva, "UQ__Facturas__0E49C69CE272320A").IsUnique();

            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdEstadoFactura).HasDefaultValue(1);
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facturas_Usuarios");

            entity.HasOne(d => d.IdEstadoFacturaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdEstadoFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facturas_EstadoFactura");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdMetodoPago)
                .HasConstraintName("FK_Facturas_MetodoPago");

            entity.HasOne(d => d.IdReservaNavigation).WithOne(p => p.Factura)
                .HasForeignKey<Factura>(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facturas_Reservas");
        });

        modelBuilder.Entity<MetodosPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodosP__6F49A9BEF6BA3A75");

            entity.ToTable("MetodosPago");

            entity.HasIndex(e => e.Nombre, "UQ__MetodosP__75E3EFCF9E0B4E4D").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reservas__0E49C69D5C798CD5");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaHoraFin).HasColumnType("datetime");
            entity.Property(e => e.FechaHoraInicio).HasColumnType("datetime");
            entity.Property(e => e.IdEstadoReserva).HasDefaultValue(1);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCanchaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCancha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservas_Cancha");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservas_Usuario");

            entity.HasOne(d => d.IdEstadoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstadoReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservas_EstadoReserva");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584C290A801E");

            entity.HasIndex(e => e.Nombre, "UQ__Roles__75E3EFCF7D1F8039").IsUnique();

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Tarifa>(entity =>
        {
            entity.HasKey(e => e.IdTarifa).HasName("PK__Tarifas__78F1A91DE7F797A0");

            entity.Property(e => e.Activa).HasDefaultValue(true);
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.DiaSemana).HasMaxLength(20);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCanchaNavigation).WithMany(p => p.Tarifas)
                .HasForeignKey(d => d.IdCancha)
                .HasConstraintName("FK_Tarifas_Canchas");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__5B65BF97DEFB6771");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A19AD37D3BC").IsUnique();

            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
