using GeoMaster.Api.DTOs;
using GeoMaster.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GeoMaster.Api.Controllers
{
    [ApiController]
    [Route("api/v1/calculos")]
    public class CalculosController : ControllerBase
    {
        private readonly ICalculadoraService _service;
        private readonly IFormaFactory _factory;

        public CalculosController(ICalculadoraService service, IFormaFactory factory)
        {
            _service = service;
            _factory = factory;
        }

        /// <summary>Calcula a área de uma forma 2D.</summary>
        [HttpPost("area")]
        [ProducesResponseType(typeof(ResultadoCalculoDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult Area([FromBody] ShapeInputDto input)
        {
            var forma = _factory.CriarForma(input);
            if (forma is null) return BadRequest(new { error = "Forma inválida ou não registrada." });

            try
            {
                var valor = _service.CalcularArea(forma);
                return Ok(new ResultadoCalculoDto("area", valor));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>Calcula o perímetro de uma forma 2D.</summary>
        [HttpPost("perimetro")]
        [ProducesResponseType(typeof(ResultadoCalculoDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult Perimetro([FromBody] ShapeInputDto input)
        {
            var forma = _factory.CriarForma(input);
            if (forma is null) return BadRequest(new { error = "Forma inválida ou não registrada." });

            try
            {
                var valor = _service.CalcularPerimetro(forma);
                return Ok(new ResultadoCalculoDto("perimetro", valor));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>Calcula o volume de uma forma 3D.</summary>
        [HttpPost("volume")]
        [ProducesResponseType(typeof(ResultadoCalculoDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult Volume([FromBody] ShapeInputDto input)
        {
            var forma = _factory.CriarForma(input);
            if (forma is null) return BadRequest(new { error = "Forma inválida ou não registrada." });

            try
            {
                var valor = _service.CalcularVolume(forma);
                return Ok(new ResultadoCalculoDto("volume", valor));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>Calcula a área superficial de uma forma 3D.</summary>
        [HttpPost("area-superficial")]
        [ProducesResponseType(typeof(ResultadoCalculoDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult AreaSuperficial([FromBody] ShapeInputDto input)
        {
            var forma = _factory.CriarForma(input);
            if (forma is null) return BadRequest(new { error = "Forma inválida ou não registrada." });

            try
            {
                var valor = _service.CalcularAreaSuperficial(forma);
                return Ok(new ResultadoCalculoDto("area_superficial", valor));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
