using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        // Declaracao de Dependencia com o DBContext (SalesWebMvcContext)
        private readonly SalesWebMvcContext _context;  /* "readonly" - Somente Leitura - Impede que a dependencia seja
                                                        * alterada */

        // Construtor para a Injecao de Dependencia
        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // Metodo para retornar todos os Department
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList(); // Ordena os Department e os lista
            // return _context.Department.ToList();
        }
    }
}
