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

        public DbSet<Apoteka> Apoteka { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<ERacun> ERacun { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<NarudzbaProizvoda> NarudzbaProizvoda { get; set; }
        public DbSet<ObradaNarudzbe> ObradaNarudzbe { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<StavkeNarudzbe> StavkeNarudzbe { get; set; }

        public DbSet<ChackOut> ChackOut { get; set; } = default!;
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

            // Povezivanje Korpa sa Proizvodom i Korisnikom
            modelBuilder.Entity<Korpa>()
                .HasOne(k => k.Proizvod)
                .WithMany()
                .HasForeignKey(k => k.ProizvodId);

            modelBuilder.Entity<Korpa>()
                .HasOne(k => k.Korisnik)
                .WithMany()
                .HasForeignKey(k => k.IDKorisnik);
        }
    }
}