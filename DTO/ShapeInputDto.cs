using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace GeoMaster.Api.DTOs
{
    public class ShapeInputDto
    {
        /// <summary>Identificador da forma (ex.: "circulo", "retangulo", "esfera").</summary>
        [Required]
        public string? Tipo { get; set; }

        /// <summary>Objeto com os parâmetros da forma (ex.: { "raio": 5 }).</summary>
        [Required]
        public JsonElement Parametros { get; set; }
    }

    public record ResultadoCalculoDto(string Operacao, double Valor);
    public record ResultadoContencaoDto(bool Contida);

    public class DuasFormasInputDto
    {
        [Required]
        public ShapeInputDto FormaExterna { get; set; } = default!;
        [Required]
        public ShapeInputDto FormaInterna { get; set; } = default!;
    }

    public class CirculoDto
    {
        [Range(0.0000001, double.MaxValue)]
        public double Raio { get; set; }
    }

    public class RetanguloDto
    {
        [Range(0.0000001, double.MaxValue)]
        public double Largura { get; set; }
        [Range(0.0000001, double.MaxValue)]
        public double Altura { get; set; }
    }

    public class EsferaDto
    {
        [Range(0.0000001, double.MaxValue)]
        public double Raio { get; set; }
    }
}
