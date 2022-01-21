using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        // Dados necessarios para uma tela de Cadastro de Vendedores
        public Seller Seller { get; set; } // Vendedor
        public ICollection<Department> Departments { get; set; } // Lista de Departamentos

    }
}
