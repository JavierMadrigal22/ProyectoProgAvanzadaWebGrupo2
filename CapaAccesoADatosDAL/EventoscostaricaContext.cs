using System;
using Microsoft.EntityFrameworkCore;
using CapaObjetos.Modelos;

namespace CapaAccesoADatosDAL
{
    public partial class EventoscostaricaContext : DbContext
    {
        public EventoscostaricaContext() { }

        public EventoscostaricaContext(DbContextOptions<EventoscostaricaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<ListaEvento> ListaEventos { get; set; }
        public virtual DbSet<Asiento> Asientos { get; set; }
        public virtual DbSet<Boleto> Boletos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioId)
                      .HasName("PK__Usuarios__2B3DE7986AF7A1B2");

                entity.ToTable(tb => tb.HasTrigger("TRG_Usuarios_Update"));

                entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D105343A3640FA")
                      .IsUnique();

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
                entity.Property(e => e.Apellido).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.Property(e => e.Estado).HasDefaultValue(true);
                entity.Property(e => e.FechaActualizacion)
                      .HasDefaultValueSql("(sysutcdatetime())");
                entity.Property(e => e.FechaCreacion)
                      .HasDefaultValueSql("(sysutcdatetime())");
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Password).HasMaxLength(256);
                entity.Property(e => e.Rol).HasMaxLength(50);
                entity.Property(e => e.Telefono).HasMaxLength(20);

                entity.HasOne(u => u.RolNavigation)
                     .WithMany()
                     .HasForeignKey(u => u.Rol)
                     .HasPrincipalKey(r => r.RolID)
                     .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Rol
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.RolID)
                      .HasName("PK__Roles__6A1A3DCCC7D5E1E3");

                entity.Property(e => e.Nombre).HasMaxLength(50);
                entity.Property(e => e.Descripcion).HasMaxLength(200);
                entity.Property(e => e.FechaCreacion)
                      .HasDefaultValueSql("SYSUTCDATETIME()");
                entity.Property(e => e.FechaActualizacion)
                      .HasDefaultValueSql("SYSUTCDATETIME()");
            });

            // Configuración de ListaEvento
            modelBuilder.Entity<ListaEvento>(entity =>
            {
                entity.ToTable("ListaEvento");

                entity.HasKey(e => e.EventoId);

                entity.Property(e => e.EventoId)
                      .HasColumnName("EventoID");

                entity.Property(e => e.Nombre)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(e => e.Ubicacion)
                      .HasMaxLength(300)
                      .IsRequired();

                entity.Property(e => e.Descripcion)
                      .HasMaxLength(1000)
                      .IsRequired();

                entity.Property(e => e.Capacidad)
                      .IsRequired();

                entity.Property(e => e.FechaHora)
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.FechaCreacion)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.Property(e => e.FechaActualizacion)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.Property(e => e.Banner)
                      .HasMaxLength(500);

                entity.Property(e => e.PrecioBase)
                      .HasColumnType("decimal(10,2)")
                      .IsRequired();

                entity.Property(e => e.Estado)
                      .IsRequired()
                      .HasDefaultValueSql("1");

                // Relaciones
                entity.HasMany(e => e.Asientos)
                      .WithOne(a => a.Evento)
                      .HasForeignKey(a => a.EventoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Boletos)
                      .WithOne(b => b.Evento)
                      .HasForeignKey(b => b.EventoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Asiento
            modelBuilder.Entity<Asiento>(entity =>
            {
                entity.ToTable("Asientos");

                entity.HasKey(e => e.AsientoId);

                entity.Property(e => e.AsientoId)
                      .HasColumnName("AsientoID");

                entity.Property(e => e.EventoId)
                      .HasColumnName("EventoID")
                      .IsRequired();

                entity.Property(e => e.Fila)
                      .IsRequired();

                entity.Property(e => e.Numero)
                      .IsRequired();

                entity.Property(e => e.EstaOcupado)
                      .HasDefaultValue(false);

                entity.Property(e => e.FechaCreacion)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("SYSUTCDATETIME()");

                // Índice único para evitar asientos duplicados
                entity.HasIndex(e => new { e.EventoId, e.Fila, e.Numero })
                      .IsUnique()
                      .HasDatabaseName("IX_Asientos_Evento_Fila_Numero");

                // Relaciones
                entity.HasOne(a => a.Evento)
                      .WithMany(e => e.Asientos)
                      .HasForeignKey(a => a.EventoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(a => a.Boletos)
                      .WithOne(b => b.Asiento)
                      .HasForeignKey(b => b.AsientoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Boleto
            modelBuilder.Entity<Boleto>(entity =>
            {
                entity.ToTable("Boletos");

                entity.HasKey(e => e.BoletoId);

                entity.Property(e => e.BoletoId)
                      .HasColumnName("BoletoID");

                entity.Property(e => e.EventoId)
                      .HasColumnName("EventoID")
                      .IsRequired();

                entity.Property(e => e.UsuarioId)
                      .HasColumnName("UsuarioID")
                      .IsRequired();

                entity.Property(e => e.AsientoId)
                      .HasColumnName("AsientoID")
                      .IsRequired();

                entity.Property(e => e.Precio)
                      .HasColumnType("decimal(10,2)")
                      .IsRequired();

                entity.Property(e => e.FechaCompra)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.Property(e => e.Estado)
                      .HasDefaultValue(true);

                // Relaciones
                entity.HasOne(b => b.Evento)
                      .WithMany(e => e.Boletos)
                      .HasForeignKey(b => b.EventoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Usuario)
                      .WithMany()
                      .HasForeignKey(b => b.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Asiento)
                      .WithMany(a => a.Boletos)
                      .HasForeignKey(b => b.AsientoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
