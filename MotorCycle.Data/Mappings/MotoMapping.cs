using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Motorcycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorCycle.Data.Mappings
{
    

    public class MotoMapping : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> modelBuilder)
        {
            modelBuilder.ToTable("Moto");
            modelBuilder.HasKey(e => e.Identificador);
            modelBuilder.Property(e => e.Ano).IsRequired();
            modelBuilder.Property(e => e.Modelo).IsRequired();
            modelBuilder.Property(e => e.Placa).IsRequired();
           

            modelBuilder.Ignore(e => e.ValidationResult);
            modelBuilder.Ignore(e => e.CascadeMode);
        }

    }
}
