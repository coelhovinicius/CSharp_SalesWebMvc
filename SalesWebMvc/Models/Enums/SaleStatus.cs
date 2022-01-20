namespace SalesWebMvc.Models.Enums
{
    public enum SaleStatus : int // Alterar "class" para "enum" e inserir " : int" a frente
    {
        Pending = 0,
        Billed = 1,
        Canceled = 2
    }
}
