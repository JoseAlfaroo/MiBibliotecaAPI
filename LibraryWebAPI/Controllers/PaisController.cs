using LibraryWebAPI.Context;
using LibraryWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaisController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var paises = await _context.Paises.ToListAsync();
                return Ok(paises);
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
                var pais = await _context.Paises.FindAsync(id);

                if(pais == null)
                {
                    return NotFound(new { message = "Pais no encontrado" });
                }

                return Ok(pais);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pais model)
        {
            var pais = new Pais
            {
                NombrePais = model.NombrePais
            };

            try
            {
                _context.Paises.Add(pais);
                await _context.SaveChangesAsync();

                return Ok(pais);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pais model)
        {
            try
            {
                var pais = await _context.Paises.FindAsync(id);

                if (pais == null)
                {
                    return NotFound(new { message = "Pais no encontrado" });
                }

                pais.NombrePais = model.NombrePais;

                _context.Paises.Update(pais);
                await _context.SaveChangesAsync();

                return Ok(pais);
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
                var pais = await _context.Paises.FindAsync(id);

                if(pais == null)
                {
                    return NotFound(new { mensaje = "Pais no encontrado" });
                }

                _context.Paises.Remove(pais);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Pais eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.ToString() });
            }
        }

    }
}
