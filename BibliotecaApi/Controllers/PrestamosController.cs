using BibliotecaApi.Data;
using BibliotecaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public PrestamosController(BibliotecaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPrestamo([FromBody] PrestamoDTO dto)
        {
            var libro = await _context.Libros.FindAsync(dto.LibroId);
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);

            if (libro == null || usuario == null)
                return BadRequest("Libro o Usuario no encontrado.");

            if (libro.EstaPrestado)
                return BadRequest("Este libro ya está prestado.");

            var prestamo = new Prestamo
            {
                LibroId = dto.LibroId,
                UsuarioId = dto.UsuarioId,
                FechaPrestamo = DateTime.Now
            };

            libro.EstaPrestado = true;

            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();

            return Ok("Préstamo registrado correctamente.");
        }


        [HttpPost("devoluciones")]
        public async Task<IActionResult> RegistrarDevolucion([FromBody] int prestamoId)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(p => p.Id == prestamoId);

            if (prestamo == null)
                return NotFound("Préstamo no encontrado.");

            if (prestamo.FechaDevolucion != null)
                return BadRequest("El libro ya fue devuelto.");

            prestamo.FechaDevolucion = DateTime.Now;
            prestamo.Libro.EstaPrestado = false;

            await _context.SaveChangesAsync();

            return Ok("Devolución registrada correctamente.");
        }
    }
}
