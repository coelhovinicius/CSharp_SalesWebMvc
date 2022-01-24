using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        // Declaracao de Dependencia com o DBContext (SalesWebMvcContext)
        private readonly SalesWebMvcContext _context;

        // Construtor para a Injecao de Dependencia
        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // Operacao Assincrona - Faz a busca dos registros de venda por data, com datas opcionais
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue) // Se a data minima for preenchida
            {
                result = result.Where(x => x.Date >= minDate.Value);// Restricao de data minima para a consulta
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller) // Join entre as tabelas
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
    }
}
