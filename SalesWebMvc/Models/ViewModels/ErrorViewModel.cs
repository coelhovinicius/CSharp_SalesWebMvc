//using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; } // Necessario para implementar uma mensagem customizada no metodo

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}