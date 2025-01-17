﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using workshop.wwwapi.Models.DatabaseModels;

namespace workshop.wwwapi.Data
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString;


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            //this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Band>().HasMany(x => x.Members).WithOne(x => x.Band).HasForeignKey(x => x.BandId);
            
            //.Navigation(e => e.ColorScheme).AutoInclude();

            ////TODO: Seed Data Here
            //Band p = new Band() { Id=1 };

            //modelBuilder.Entity<Band>().HasData(p);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "Database");
            optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
            
        }


        public DbSet<Band> Bands { get; set; }
        public DbSet<BandMember> BandMembers { get; set; }

    }
}
