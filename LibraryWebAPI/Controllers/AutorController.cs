using LibraryWebAPI.Context;
using LibraryWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AutorController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var autores = await _context.Autores.ToListAsync();
                return Ok(autores);
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
                var autor = await _context.Autores.FindAsync(id);

                if (autor == null)
                {
                    return NotFound(new { message = "Autor no encontrado" });
                }

                return Ok(autor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }

        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Autor model)
        {
            var autor = new Autor
            {
                NombresAutor = model.NombresAutor,
                ApellidosAutor = model.ApellidosAutor,
                PaisID = model.PaisID
            };

            try
            {
                _context.Autores.Add(autor);
                await _context.SaveChangesAsync();

                return Ok(autor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Autor model)
        {
            try
            {
                var autor = await _context.Autores.FindAsync(id);

                if (autor == null)
                {
                    return NotFound(new { message = "Autor no encontrado" });
                }

                autor.NombresAutor = model.NombresAutor;
                autor.ApellidosAutor = model.ApellidosAutor;
                autor.PaisID = model.PaisID;

                _context.Autores.Update(autor);
                await _context.SaveChangesAsync();

                return Ok(autor);
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
                var autor = await _context.Autores.FindAsync(id);

                if (autor == null)
                {
                    return NotFound(new { message = "Autor no encontrado" });
                }

                _context.Autores.Remove(autor);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Autor eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }
    }
}
