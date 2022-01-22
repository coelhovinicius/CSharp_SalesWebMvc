using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
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
        public IActionResult Index() // Chama o Controlador
        {
            var list = _sellerService.FindAll(); // Implementacao da chamada "_sellerService.FindAll" - acessa do Model
            return View(list); // Gera um "IActionResult" contendo a lista "list" - Encaminha os dados para a View
        }

        public IActionResult Create() // Acao correspondente ao metodo GET do HTTP - Nao permite a edicao de dados
        {
            var departments = _departmentService.FindAll(); // Busca todos os dados no servico "_departmentService"
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel); // Retorna a View de nome "Create"
        }

        [HttpPost] // Anotacao que indica que e um metodo POST
        [ValidateAntiForgeryToken] // Evita ataques CSRF (Cross-Site Requesting Forgery - protege a sessao de autenticacao)
        public IActionResult Create(Seller seller) // Metodo POST do HTTP - Permite a edicao de dados
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Retorna para o Index
        }

        public IActionResult Delete(int? id) // "int?" indica que e opcional
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
