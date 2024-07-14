using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Models
{
    public class Libro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? LibroID { get; set; }
        [Required]
        public string? Titulo { get; set; }
        [Required]
        public int? AñoPublicacion { get; set; }
        public string? Portada { get; set; }
        public string? Descripcion { get; set; }
    }
}
