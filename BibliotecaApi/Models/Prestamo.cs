using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BibliotecaApi.Models
{
    public class Prestamo
    {
        public int Id { get; set; }

        [Required]
        public int LibroId { get; set; }

        [JsonIgnore]
        public Libro Libro { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [JsonIgnore]
        public Usuario Usuario { get; set; }

        [Required]
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;

        public DateTime? FechaDevolucion { get; set; }
    }
}
