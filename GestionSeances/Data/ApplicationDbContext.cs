using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionSeances.Models;

namespace GestionSeances.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Kine> Kines { get; set; }
        public DbSet<Seance> Seances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships (Fluent API)
            modelBuilder.Entity<Seance>()
                .HasOne(s => s.Kine)
                .WithMany()
                .HasForeignKey(s => s.IdK);

            modelBuilder.Entity<Seance>()
                .HasOne(s => s.Patient)
                .WithMany()
                .HasForeignKey(s => s.IdP);
        }
    }
}