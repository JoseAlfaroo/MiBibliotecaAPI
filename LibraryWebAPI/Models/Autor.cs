using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Models
{
    public class Autor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? AutorID { get; set; }
        [Required]
        public string? NombresAutor { get; set; }
        [Required]
        public string? ApellidosAutor { get; set; }

        //Relacion con pais
        public int? PaisID { get; set; }
        public Pais? Pais { get; set; }
    }
}
