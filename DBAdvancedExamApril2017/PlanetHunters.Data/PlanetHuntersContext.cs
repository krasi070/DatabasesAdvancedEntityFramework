namespace PlanetHunters.Data
{
    using Models;
    using System.Data.Entity;

    public class PlanetHuntersContext : DbContext
    {
        public PlanetHuntersContext()
            : base("name=PlanetHuntersContext")
        {
            
        }

        public virtual DbSet<Astronomer> Astronomers { get; set; }

        public virtual DbSet<Discovery> Discoveries { get; set; }

        public virtual DbSet<Telescope> Telescopes { get; set; }

        public virtual DbSet<StarSystem> StarSystems { get; set; }

        public virtual DbSet<Star> Stars { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Astronomer>()
                .HasMany(a => a.PioneeringDiscoveries)
                .WithMany(d => d.Pioneers)
                .Map(cs =>
                {
                    cs.ToTable("PioneerDiscoveries");
                });

            modelBuilder.Entity<Astronomer>()
                .HasMany(a => a.ObservedDiscoveries)
                .WithMany(d => d.Observers)
                .Map(cs =>
                {
                    cs.ToTable("ObserverDiscoveries");
                });

            modelBuilder.Entity<Discovery>()
                .HasMany(d => d.Stars)
                .WithMany(s => s.DiscoveriesIncludedIn)
                .Map(cs =>
                {
                    cs.ToTable("DiscoveryStars");
                });

            modelBuilder.Entity<Discovery>()
                .HasMany(d => d.Planets)
                .WithMany(p => p.DiscoveriesIncludedIn)
                .Map(cs =>
                {
                    cs.ToTable("DiscoveryPlanets");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}