using LibraryWebAPI.Context;
using LibraryWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibroController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var libros = await _context.Libros
                    .Include(l => l.GenerosLibro).ThenInclude(gl => gl.Genero)
                    .Include(l => l.AutoresLibro).ThenInclude(al => al.Autor)
                    .ToListAsync();

                return Ok(libros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var libro = await _context.Libros
                    .Include(l => l.GenerosLibro).ThenInclude(gl => gl.Genero)
                    .Include(l => l.AutoresLibro).ThenInclude(al => al.Autor)
                    .FirstOrDefaultAsync(l => l.LibroID == id);

                if (libro == null)
                {
                    return NotFound(new { message = "Libro no encontrado" });
                }

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Libro model)
        {
            var libro = new Libro
            {
                Titulo = model.Titulo,
                AñoPublicacion = model.AñoPublicacion,
                Portada = model.Portada,
                Descripcion = model.Descripcion
            };

            try
            {
                _context.Libros.Add(libro);
                await _context.SaveChangesAsync();

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Libro model)
        {
            try
            {
                var libro = await _context.Libros.FindAsync(id);

                if (libro == null)
                {
                    return NotFound(new { message = "Libro no encontrado" });
                }

                libro.Titulo = model.Titulo;
                libro.AñoPublicacion = model.AñoPublicacion;
                libro.Portada = model.Portada;
                libro.Descripcion = model.Descripcion;


                _context.Libros.Update(libro);
                await _context.SaveChangesAsync();

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var libro = await _context.Libros.FindAsync(id);

                if (libro == null)
                {
                    return NotFound(new { mensaje = "Libro no encontrado" });
                }

                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Libro eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        
    }
}
