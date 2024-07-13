using LibraryWebAPI.Context;
using LibraryWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GeneroController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var generos = await _context.Generos.ToListAsync();
                return Ok(generos);
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
                var genero = await _context.Generos.FindAsync(id);

                if (genero == null)
                {
                    return NotFound(new { message = "Genero no encontrado" });
                }

                return Ok(genero);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }

        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Genero model)
        {
            var genero = new Genero
            {
                NombreGenero = model.NombreGenero
            };

            try
            {
                _context.Generos.Add(genero);
                await _context.SaveChangesAsync();

                return Ok(genero);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Genero model)
        {
            try
            {
                var genero = await _context.Generos.FindAsync(id);

                if (genero == null)
                {
                    return NotFound(new { message = "Genero no encontrado" });
                }
                genero.NombreGenero = model.NombreGenero;

                _context.Generos.Update(genero);
                await _context.SaveChangesAsync();

                return Ok(genero);
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
                var genero = await _context.Generos.FindAsync(id);

                if (genero == null)
                {
                    return NotFound(new { message = "Genero no encontrado" });
                }

                _context.Generos.Remove(genero);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Genero eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }
    }
}
