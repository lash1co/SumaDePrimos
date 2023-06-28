using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Filtro
    {
        [StringLength(100,ErrorMessage = "La longitud máxima del campo es de 100 caracteres.")]
        public string? Usuario { get; set; }
        
        [Range(0,9999999999, ErrorMessage = "El rango máximo debe estar entre 0 y menor a 100 millones")]
        public long? RespuestaMin { get; set; }

        [Range(0,9999999999, ErrorMessage = "El rango mínimo debe estar entre 0 y menor a 100 millones")]
        public long? RespuestaMax { get; set; }
    }
}
