namespace MotoRental.API.ViewModels
{
    public class RentalViewModelPriceLess
    {
        public int IdCliente { get; set; }
        public int IdMotocicleta { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Observacoes { get; set; }
    }
}
