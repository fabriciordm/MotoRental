using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MotorCycle.Data.Mappings
{
    public class DeliveryDriverMapping : IEntityTypeConfiguration<DeliveryDriver>
    {
        public void Configure(EntityTypeBuilder<DeliveryDriver> modelBuilder)
        {
            modelBuilder.ToTable("DeliveryDriver");
            modelBuilder.HasKey(e => e.Identificador);
            modelBuilder.Property(e => e.Nome).IsRequired();
            modelBuilder.Property(e => e.CNPJ).IsRequired();
            modelBuilder.Property(e => e.DataNascimento).IsRequired();
            modelBuilder.Property(e => e.NumeroCNH).IsRequired();
            modelBuilder.Property(e => e.TipoCNH).IsRequired();
            modelBuilder.Property(e => e.ImagemCNH).IsRequired();

          
        }
    }
}
