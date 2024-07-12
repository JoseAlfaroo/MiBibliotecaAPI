using LibraryWebAPI.Context;
using LibraryWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}
