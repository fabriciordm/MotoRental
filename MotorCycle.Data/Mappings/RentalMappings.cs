using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Data.Mappings
{
    public  class RentalMappings : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> modelBuilder)
        {
            // Nome da tabela no banco de dados
            modelBuilder.ToTable("Rental");

            // Chave primária
            modelBuilder.HasKey(l => l.IdLocacao);

            // Mapeamento das propriedades
            modelBuilder.Property(l => l.IdLocacao).HasColumnName("IdLocacao").IsRequired();
            modelBuilder.Property(l => l.IdCliente).HasColumnName("IdCliente").IsRequired(); 
            modelBuilder.Property(l => l.IdMotocicleta).HasColumnName("IdMotocicleta").IsRequired(); 
            modelBuilder.Property(l => l.DataInicio).HasColumnName("DataInicio").IsRequired(); 
            modelBuilder.Property(l => l.DataFim).HasColumnName("DataFim").IsRequired(); 
            modelBuilder.Property(l => l.Preco).HasColumnName("Preco").IsRequired(); 
            modelBuilder.Property(l => l.Observacoes).HasColumnName("Observacoes");
        }
    }
}
