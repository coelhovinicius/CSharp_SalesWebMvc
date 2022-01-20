using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; } /* Associacao Varios para 1 -  Cada "Seller" pode possuir, apenas,
                                                    * um "Department", e cada "Department" pode possuir diversos "Seller" */
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); /* Associacao 1 para Varios - Cada 
                                                                                        * "Seller" pode possuir diversos 
                                                                                        * "SalesRecord", porem, cada 
                                                                                        * "SalesRecord so pode pertencer 
                                                                                        * a 1 "Seller" */

        // Construtores
        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        // Metodos Customizados
        public void AddSales(SalesRecord sr) // Operacao para adicionar uma venda na lista de vendas 
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        // Filtrar a lista para obter as vendas num periodo e retornar o valor total das vendas - Utilizacao do LINQ
        public double TotalSales(DateTime initial, DateTime final)
        {
            // De todas as vendas do vendedor entre as datas "initial" e "final", faz a soma (".Amount")
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount); // Expressao Lambda
        }
    }
}
