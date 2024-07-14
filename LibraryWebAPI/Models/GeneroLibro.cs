using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebAPI.Models
{
    public class GeneroLibro
    {
        [Key, Column(Order = 0)]
        public int LibroID { get; set; }
        public Libro? Libro { get; set; }

        [Key, Column(Order = 1)]
        public int GeneroID { get; set; }
        public Genero? Genero { get; set; }
    }
}
