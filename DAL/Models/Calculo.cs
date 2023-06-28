
namespace DAL.Models;

public partial class Calculo
{
    public int Id { get; set; }

    public string? Usuario { get; set; }

    public long? Respuesta { get; set; }

    public DateTime? Fecha { get; set; }
}
