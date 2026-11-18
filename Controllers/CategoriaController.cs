
using Microsoft.AspNetCore.Mvc;
using VentasApi.Models.DTOS.CategoriaDTO;
using VentasApi.Services.IServices;

namespace VentasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _CapService;

        public CategoriaController(ICategoriaService CapService)
        {
            _CapService = CapService;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<CategoriaDTO>>> GetAll()
        {
            var resultado = await _CapService.GetAll();

            if(!resultado.IsSuccess)
                return BadRequest(resultado.Message);
            
            return Ok(resultado.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> GetById(int id)
        {
            var resultado = await _CapService.GetById(id);

            if(!resultado.IsSuccess)
                return NotFound(resultado.Message);
            
            
            return Ok(resultado.Data);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Add(CrearCategoriaDTO dTO)
        {
            var resultado = await _CapService.Add(dTO);

            if(!resultado.IsSuccess)
                return BadRequest(resultado.Message);
            
            return CreatedAtAction(nameof(GetById), new { id = resultado.Data!.ID }, resultado.Data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Update(int id,UpdateCategoriaDTO dTO)
        {
            var resultado = await _CapService.Update(id, dTO);

            if(!resultado.IsSuccess)
                return NotFound(resultado.Message);
            
            return Ok(resultado.Data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var resultado = await _CapService.Delete(id);

            if(!resultado.IsSuccess)
                return BadRequest(resultado.Message);

            return Ok(resultado.Data);
        }
    }
}
