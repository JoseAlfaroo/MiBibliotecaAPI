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


        // Metodo para agregar generos del libro
        [HttpPost("{libroID}/generos/{generoID}")]
        public async Task<IActionResult> AddGenero(int libroID, int generoID)
        {
            try
            {
                var libro = await _context.Libros
                    .Include(l => l.GenerosLibro)
                    .FirstOrDefaultAsync(l => l.LibroID == libroID);

                if(libro == null)
                {
                    return NotFound(new { message = "Libro no encontrado" });
                }

                var genero = await _context.Generos.FindAsync(generoID);
                
                if (genero == null)
                {
                    return NotFound(new { message = "Genero no encontrado" });
                }

                libro.GenerosLibro?.Add(new GeneroLibro { Genero = genero });

                await _context.SaveChangesAsync();

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        // Metodo para eliminar generos del libro
        [HttpDelete("{libroID}/generos/{generoID}")]
        public async Task<IActionResult> RemoveGenero(int libroID, int generoID)
        {
            try
            {
                var libro = await _context.Libros
                    .Include(l => l.GenerosLibro)
                    .FirstOrDefaultAsync(l => l.LibroID == libroID);

                if (libro == null)
                {
                    return NotFound(new { message = "Libro no encontrado" });
                }

                var generoLibro = libro.GenerosLibro?.FirstOrDefault(gl => gl.GeneroID == generoID);

                if (generoLibro == null)
                {
                    return NotFound(new { message = "El libro no tiene este género" });
                }

                libro.GenerosLibro?.Remove(generoLibro);

                await _context.SaveChangesAsync();

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        // Metodo para agregar autores del libro
        [HttpPost("{libroID}/autores/{autorID}")]
        public async Task<IActionResult> AddAutor(int libroID, int autorID)
        {
            try
            {
                var libro = await _context.Libros
                    .Include(l => l.AutoresLibro)
                    .FirstOrDefaultAsync(l => l.LibroID == libroID);

                if (libro == null)
                {
                    return NotFound(new { message = "Libro no encontrado" });
                }

                var autor = await _context.Autores.FindAsync(autorID);

                if (autor == null)
                {
                    return NotFound(new { message = "Autor no encontrado" });
                }

                libro.AutoresLibro?.Add(new AutorLibro { Autor = autor });

                await _context.SaveChangesAsync();

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        // Metodo para eliminar autores del libro
        [HttpDelete("{libroID}/autores/{autorID}")]
        public async Task<IActionResult> RemoveAutor(int libroID, int autorID)
        {
            try
            {
                var libro = await _context.Libros
                    .Include(l => l.AutoresLibro)
                    .FirstOrDefaultAsync(l => l.LibroID == libroID);

                if (libro == null)
                {
                    return NotFound(new { message = "Libro no encontrado" });
                }

                var autorLibro = libro.AutoresLibro?.FirstOrDefault(gl => gl.AutorID == autorID);

                if (autorLibro == null)
                {
                    return NotFound(new { message = "El libro no tiene este autor" });
                }

                libro.AutoresLibro?.Remove(autorLibro);

                await _context.SaveChangesAsync();

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }
    }
}
