using DnevnaDoza.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DnevnaDoza.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Apoteka> Apoteka{ get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<ERacun> ERacun { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<NarudzbaProizvoda> NarudzbaProizvoda { get; set; }
        public DbSet<ObradaNarudzbe> ObradaNarudzbe { get; set; }

        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<StavkeNarudzbe> StavkeNarudzbe { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apoteka>().ToTable("Apoteka");
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<ERacun>().ToTable("ERacun");
            modelBuilder.Entity<Korpa>().ToTable("Korpa");
            modelBuilder.Entity<NarudzbaProizvoda>().ToTable("NarudzbaProizvoda");
            modelBuilder.Entity<ObradaNarudzbe>().ToTable("ObradaNarudzbe");
            modelBuilder.Entity<Proizvod>().ToTable("Proizvod");
            modelBuilder.Entity<StavkeNarudzbe>().ToTable("StavkeNarudzbe");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DnevnaDoza.Models.ChackOut> ChackOut { get; set; } = default!;
    }
}
