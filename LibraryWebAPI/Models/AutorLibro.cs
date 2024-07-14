using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryWebAPI.Models
{
    public class AutorLibro
    {
        [Key, Column(Order = 0)]
        public int LibroID { get; set; }
        [JsonIgnore]
        public Libro? Libro { get; set; }

        [Key, Column(Order = 1)]
        public int AutorID { get; set; }
        public Autor? Autor { get; set; }
    }
}
