using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Models // Apesar de estar na pasta Data, pertence ao namespace Models
{
    public class SalesWebMvcContext : DbContext // Heranca - Permite encapsular uma secao com o banco de dados
    {
        public SalesWebMvcContext(DbContextOptions<SalesWebMvcContext> options)
            : base(options)
        {
        }

        // Para que o Entity Framework reconheca o Modelo de Dominio que foi feito, devem-se inserir os DBSets das entidades
        public DbSet<Department> Department { get; set; } // DBSet da entidade Department
        public DbSet<Seller> Seller { get; set; } // DBSet da entidade Seller
        public DbSet<SalesRecord> SalesRecord { get; set; } // DBSet da entidade SalesRecord
    }
}
