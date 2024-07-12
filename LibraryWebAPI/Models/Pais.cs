using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Models
{
    public class Pais
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? PaisID { get; set; }
        [Required]
        public string? NombrePais { get; set; }
    }
}
