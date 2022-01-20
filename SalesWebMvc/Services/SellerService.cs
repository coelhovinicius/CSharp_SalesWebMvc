using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        // Declaracao de Dependencia com o DBContext (SalesWebMvcContext)
        private readonly SalesWebMvcContext _context;  /* "readonly" - Somente Leitura - Impede que a dependencia seja
                                                        * alterada */

        // Construtor para a Injecao de Dependencia
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // Metodo "FindAll" para retornar a lista de vendedores do banco de dados - Forma simples
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); /* Acessa a fonte de dados relacionada a tabela de vendedores e converte em
                                              * uma Lista */
        }
    }
}
