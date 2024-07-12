using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        
        // Puede ser NULL para permitir autores sin pais, tanto para listado como para registro y actualización
        public int? PaisID { get; set; }

        //Relacion con pais
        [JsonIgnore]
        public Pais? Pais { get; set; }
    }
}
