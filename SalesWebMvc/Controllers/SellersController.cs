using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // Dependencias
        private readonly SellerService _sellerService;

        // Construtores
        public SellersController(SellerService sellerService) // Injecao de Dependencia
        {
            _sellerService = sellerService;
        }

        // Metodos - Operacoes - Acoes
        public IActionResult Index() // Chama o Controlador
        {
            var list = _sellerService.FindAll(); // Implementacao da chamada "_sellerService.FindAll" - acessa do Model
            return View(list); // Gera um "IActionResult" contendo a lista "list" - Encaminha os dados para a View
        }

        public IActionResult Create() // Acao correspondente ao metodo GET do HTTP - Nao permite a edicao de dados
        {
            return View(); // Retorna a View de nome "Create"
        }

        [HttpPost] // Anotacao que indica que e um metodo POST
        [ValidateAntiForgeryToken] // Evita ataques CSRF (Cross-Site Requesting Forgery - protege a sessao de autenticacao)
        public IActionResult Create(Seller seller) // Metodo POST do HTTP - Permite a edicao de dados
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Retorna para o Index
        }
    }
}
