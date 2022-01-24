using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // Dependencias
        private readonly SellerService _sellerService; // Dependencia para SellerService
        private readonly DepartmentService _departmentService; // Dependencia para DepartmentService

        // Construtores
        public SellersController(SellerService sellerService, DepartmentService departmentService) // Injecao de Dependencia
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // Metodos - Operacoes - Acoes
        // Metodo Assincrono
        public async Task<IActionResult> Index() // Chama o Controlador
        {
            var list = await _sellerService.FindAllAsync(); // Implementacao da chamada "_sellerService.FindAll" - acessa do Model
            return View(list); // Gera um "IActionResult" contendo a lista "list" - Encaminha os dados para a View
        }

        /*public IActionResult Index() // Chama o Controlador
        {
            var list = _sellerService.FindAll(); // Implementacao da chamada "_sellerService.FindAll" - acessa do Model
            return View(list); // Gera um "IActionResult" contendo a lista "list" - Encaminha os dados para a View
        }*/

        // GET
        //Metodo Assincrono
        public async Task<IActionResult> Create() // Acao correspondente ao metodo GET do HTTP - Nao permite a edicao de dados
        {
            var departments = await _departmentService.FindAllAsync(); // Busca todos os dados no servico "_departmentService"
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel); // Retorna a View de nome "Create"
        }

        /*public IActionResult Create() // Acao correspondente ao metodo GET do HTTP - Nao permite a edicao de dados
        {
            var departments = _departmentService.FindAll(); // Busca todos os dados no servico "_departmentService"
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel); // Retorna a View de nome "Create"
        }*/

        [HttpPost] // Anotacao que indica que e um metodo POST
        [ValidateAntiForgeryToken] // Evita ataques CSRF (Cross-Site Requesting Forgery - protege a sessao de autenticacao)
        // Metodo Async
        public async Task<IActionResult> Create(Seller seller) // Metodo POST do HTTP - Permite a edicao de dados
        {
            // Validacao de Dados (caso o JavaScript esteja desabilitado)
            if (!ModelState.IsValid) // Se nao for validado
            {
                var departments = await _departmentService.FindAllAsync(); // Carrega os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel); // Repassa a mesma View ate o preenchimento correto do Formulario
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index)); // Retorna para o Index
        }

        /*public IActionResult Create(Seller seller) // Metodo POST do HTTP - Permite a edicao de dados
        {
            // Validacao de Dados (caso o JavaScript esteja desabilitado)
            if (!ModelState.IsValid) // Se nao for validado
            {
                var departments = _departmentService.FindAll(); // Carrega os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel); // Repassa a mesma View ate o preenchimento correto do Formulario
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Retorna para o Index
        }*/

        // GET
        //Metodo Async
        public async Task<IActionResult> Delete(int? id) // "int?" indica que e opcional
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        /*public IActionResult Delete(int? id) // "int?" indica que e opcional
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }*/

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Metodo Async
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        /*public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }*/

        // Metodo Async

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        /*public IActionResult Details(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }*/

        //Metodo Async
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        /*{
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }*/

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Metodo Async
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            // Validacao de Dados (caso o JavaScript esteja desabilitado)
            if (!ModelState.IsValid) // Se nao for validado
            {
                var departments = await _departmentService.FindAllAsync(); // Carrega os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel); // Repassa a mesma View ate o preenchimento correto do Formulario
            }
            if (id != seller.Id)
            {
                //return BadRequest();
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            //catch (NotFoundException)
            //catch (NotFoundException e)
            /* ApplicationException e um Supertipo das duas excecoes. Por meio de Upcasting, as duas excecoes podem
             * se relacionar com ApplicationException */
            catch (ApplicationException e) // Permite o tratamento de diversas excecoes que retornem o mesmo objeto
            {
                // Redireciona para a pagina de erro, passando a mensagem de excecao
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            /*//catch (DbConcurrencyException)
            catch (DbConcurrencyException e)
            {
                //return BadRequest();
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }*/
        }

        // Acao de erro - Nao precisa ser assincrona, pois nao ha acesso adados, apenas o retorno da View
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // Obtem o Id interno da requisicao
            };
            return View(viewModel);
        }
    }
}
