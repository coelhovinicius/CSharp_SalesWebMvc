using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")] // Annotation para denotar que o campo e obrigatorio
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")] // Limite de tamanho
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")] // Annotation para exibir "Birth Date" como nome do atributo
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 10000000.0, ErrorMessage = "{0} must be from {1} to {2}")] // Range do salario
        [Display(Name = "Base Salary")] // Annotation
        [DisplayFormat(DataFormatString = "{0:F2}")] // "0" indica o valor do atributo e "F2" indica que tera duas casas
        public double BaseSalary { get; set; }
        public Department Department { get; set; } /* Associacao Varios para 1 -  Cada "Seller" pode possuir, apenas,
                                                    * um "Department", e cada "Department" pode possuir diversos "Seller" */
        public int DepartmentId { get; set; } // Garante a Integridade Referencial (deve ser not null)
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
