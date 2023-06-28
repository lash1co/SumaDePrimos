using DAL.Models;

namespace CalculosMVC.Models
{
    public class InformationViewModel
    {
        public Filtro Filtro { get; set; }
        public IEnumerable<Calculo> Calculos { get; set; } = new List<Calculo>();
    }
}
