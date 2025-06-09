using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Models
{
    public class PrestamoDTO
    {
        [Required]
        public int LibroId { get; set; }

        [Required]
        public int UsuarioId { get; set; }
    }
}