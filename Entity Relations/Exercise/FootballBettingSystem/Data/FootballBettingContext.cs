namespace P03_FootballBetting.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext(DbContextOptions options)
        : base(options)
        {
        }

        public FootballBettingContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
