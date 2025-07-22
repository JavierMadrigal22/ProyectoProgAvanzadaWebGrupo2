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

                entity.HasKey(e => e.EventoId); // usa la columna real 'EventoID'

                entity.Property(e => e.EventoId)
                      .HasColumnName("EventoID"); // OJO: aquí es EventoID, no Id

                entity.Property(e => e.Nombre)
                      .HasMaxLength(100)
                      .IsFixedLength();

                entity.Property(e => e.Ubicacion)
                      .HasMaxLength(100)
                      .IsFixedLength();

                entity.Property(e => e.Descripcion)
                      .HasMaxLength(100)
                      .IsFixedLength();

                entity.Property(e => e.Capacidad);

                entity.Property(e => e.FechaHora)
                      .HasMaxLength(100)
                      .IsFixedLength();

                entity.Property(e => e.FechaCreacion)
                      .HasColumnType("datetime");

                entity.Property(e => e.FechaActualizacion)
                      .HasColumnType("datetime");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
