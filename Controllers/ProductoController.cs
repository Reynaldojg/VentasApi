using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentasApi.Models.DTOS.ProductoDTO;
using VentasApi.Services.IServices;

namespace VentasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServices _ProdService;

        public ProductoController(IProductoServices ProdService)
        {
            _ProdService = ProdService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProductoDTO>>> GetAll()
        {
            var resultado = await _ProdService.GetAll();

            if(!resultado.IsSuccess)
                return BadRequest(resultado.Message);
            
            return Ok(resultado.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var resultado = await _ProdService.GetById(id);

            if(!resultado.IsSuccess)
                return BadRequest(resultado.Message);
            
            return Ok(resultado.Data);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDTO>>Add(CrearProductoDTO dTO)
        {
            var resultado = await _ProdService.Add(dTO);

            if(!resultado.IsSuccess)
                return BadRequest(resultado.Message);
            
            return CreatedAtAction(nameof(GetById), new {id = resultado.Data!.ID}, resultado.Data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductoDTO>> Update(int id, UpdateProductoDTO dTO)
        {
            var resultado = await _ProdService.Update(id, dTO);

            if(!resultado.IsSuccess)
                return NotFound(resultado.Message);
            
            return Ok(resultado.Data);
        }

                [HttpDelete("{id}")]
        public async Task<ActionResult<ProductoDTO>> Delete(int id)
        {
            var resultado = await _ProdService.Delete(id);

            if(!resultado.IsSuccess)
                return BadRequest(resultado.Message);

            return Ok(resultado.Data);
        }



    }
}
