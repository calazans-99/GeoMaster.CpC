using GeoMaster.Api.DTOs;
using GeoMaster.Api.Services;
using GeoMaster.Api.Services.Contencao;
using Microsoft.AspNetCore.Mvc;

namespace GeoMaster.Api.Controllers
{
    [ApiController]
    [Route("api/v1/validacoes")]
    public class ValidacoesController : ControllerBase
    {
        private readonly IFormaFactory _factory;
        private readonly IContencaoResolver _resolver;

        public ValidacoesController(IFormaFactory factory, IContencaoResolver resolver)
        {
            _factory = factory;
            _resolver = resolver;
        }

        /// <summary>Verifica se a forma interna está contida na forma externa.</summary>
        [HttpPost("forma-contida")]
        [ProducesResponseType(typeof(ResultadoContencaoDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult FormaContida([FromBody] DuasFormasInputDto input)
        {
            var externa = _factory.CriarForma(input.FormaExterna);
            var interna = _factory.CriarForma(input.FormaInterna);
            if (externa is null || interna is null) return BadRequest(new { error = "Forma inválida ou não registrada." });

            var strategy = _resolver.Resolve(externa.GetType(), interna.GetType());
            if (strategy is null) return BadRequest(new { error = "Par de formas ainda não suportado." });

            var ok = strategy.Contida(externa, interna);
            return Ok(new ResultadoContencaoDto(ok));
        }
    }
}
