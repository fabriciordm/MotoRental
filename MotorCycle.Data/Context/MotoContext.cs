using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Motorcycle.Domain.Models;
using MotorCycle.Data.Mappings;
using MotoRental.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MotorCycle.Data.Context
{
    public class MotoContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public MotoContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<DeliveryDriver> DeliveryDrivers { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeliveryDriverMapping());
            modelBuilder.ApplyConfiguration(new MotoMapping());
            modelBuilder.ApplyConfiguration(new RentalMappings());


            modelBuilder.Entity<Rental>()
         .Property(d => d.IdLocacao)
         .ValueGeneratedOnAdd();

            modelBuilder.Entity<Rental>()
            .HasKey(d => d.IdLocacao);

            modelBuilder.Entity<DeliveryDriver>()
           .Property(d=> d.Identificador)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Moto>()
            .HasKey(d => d.Identificador);

            modelBuilder.Entity<Moto>()
          .Property(m => m.Identificador)
          .ValueGeneratedOnAdd(); 

            modelBuilder.Entity<Moto>()
            .HasKey(m => m.Identificador); // Define a propriedade Identificador como a chave primária


            base.OnModelCreating(modelBuilder);
        }

       
    }
}

