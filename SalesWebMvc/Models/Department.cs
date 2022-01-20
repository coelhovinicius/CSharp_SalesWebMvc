using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); /* Associacao 1 para Varios - Cada
                                                                                * "Department" pode possuir diversos
                                                                                * "Sellers" e cada "Seller" pode, apenas, 
                                                                                * possuir 1 "Department" */

        // Construtores
        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // Metodos Customizados
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        /*public void RemoveSeller(Seller seller)
        {
            Sellers.Remove(seller);
        }*/

        public double TotalSales(DateTime initial, DateTime final) // Calcula as vendas entre "initial" e "final"
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final)); // Expressao Lambda
        }
    }
}
