using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException // Heranca para tratamento das excecoes
    {
        //Excessoes personalizadas
        public NotFoundException(string message) : base(message) // Manda a excecao para a classe base
        {
        }
    }
}
