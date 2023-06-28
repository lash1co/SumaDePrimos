using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Registro
    {
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(100, ErrorMessage = "La longitud máxima del campo es de 100 caracteres.")]
        public string Usuario { get; set; }
        
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Range(0,99999, ErrorMessage = "El límite debe ser un número entre 0 y 99999.")]
        public int Limite { get; set; }
    }
}
