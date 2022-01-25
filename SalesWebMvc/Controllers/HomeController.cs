using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class HomeController : Controller // Heranca
    {
        // Metodos que retornam um objeto do tipo "IActionResult", que e um resultado de uma acao
        public IActionResult Index() // Acao "Index"
        {
            return View(); // Method Builder - Metodo auxiliar que retorna um objeto tipo "IActionResult", no caso uma "View"
        }

        public IActionResult About() // Acao "About"
        {
            // ViewData e um dicionario do C#, sendo um acolecao de chaves "pares-valor"
            // ViewData["Title"] = "About";
            // Acessa o objeto "ViewData", e esse objeto, na chave "Message", recebera o valor "Your application..."
            ViewData["Message"] = "Sales Web MVC App with C#"; // Acrescenta um valor ao ViewData
            ViewData["Developer"] = "Vinícius Coelho Bemfica"; // Acrescenta um valor ao ViewData

            return View(); 
                // Method Builder - Metodo auxiliar que retorna um objeto tipo "IActionResult", no caso uma "View"
        }

        public IActionResult Contact() // Acao "Contact"
        {
            ViewData["Message"] = "Vinícius Coelho Bemfica";

            return View();
        }

        public IActionResult Privacy() // Acao "Privacy"
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
