using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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

        // Metodo "FindAllAsync()" - Metodo Assincrono
        public async Task<List<Seller>> FindAllAsync() // "async" - "Task" - "FindAllAsync()"
        {
            return await _context.Seller.ToListAsync(); // "await" - "ToListAsync"
        }

        /* // Metodo "FindAll" para retornar a lista de vendedores do banco de dados - Forma simples
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); // Acessa a fonte de dados relacionada a tabela de vendedores e converte em
                                             // uma Lista 
        }*/

        //Metodo para inserir um novo vendedor no banco de dados
        // Metodo Assincrono
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj); // Operacao feita em memoria
            await _context.SaveChangesAsync(); // Acesso do Banco de Dados e Gravacao
        }

        /*public void Insert(Seller obj)
        {
            // obj.Department = _context.Department.First(); // Associa o primeiro Department do BD ao vendedor 
            _context.Add(obj); // Adiciona as alteracoes
            _context.SaveChanges(); // Confirma as alteracoes
        }*/

        // Metodo Assincrono
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        /*public Seller FindById(int id)
        {
            // Eager Loading - Carrega outros objetos associados ao objeto principal
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }*/

        // Metodo Assincrono
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                //throw new IntegrityException("Cannot delete this seller because there are sales on his/her name");
                throw new IntegrityException(e.Message + "\nError: Seller already has sales.");
            }
        }

        /*public void Remove(int id)
        {
            var obj = _context.Seller.Find(id); // Chama o objeto
            _context.Seller.Remove(obj); // Remove o objeto
            _context.SaveChanges(); // Salva as alteracoes - Efetivar a alteracao
        }*/

        // Metodo Assincrono
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            // Testa se existe no BD algum vendedor "x" cujo "Id" seja igual ao "Id" de "obj" 
            if (!hasAny) // Caso nao exista
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            /* Interceptar uma excessao do nivel de acesso a dados e relancar essa excecao, utilizando uma excecao em
             * nivel de Servico */
            catch (DbUpdateConcurrencyException e) // Intercepta a excecao de acesso a dados
            {
                throw new DbConcurrencyException(e.Message); // Lanca a excecao da camada de Servico
            }
        }
        /*public void Update(Seller obj)
        {
            // Testa se existe no BD algum vendedor "x" cujo "Id" seja igual ao "Id" de "obj" 
            if (!_context.Seller.Any(x => x.Id == obj.Id)) // Caso nao exista
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            // Interceptar uma excessao do nivel de acesso a dados e relancar essa excecao, utilizando uma excecao em
            // nivel de Servico 
            catch (DbUpdateConcurrencyException e) // Intercepta a excecao de acesso a dados
            {
                throw new DbConcurrencyException(e.Message); // Lanca a excecao da camada de Servico
    }
}*/
    }
}
