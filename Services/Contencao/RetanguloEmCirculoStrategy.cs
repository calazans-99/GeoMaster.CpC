using GeoMaster.Api.Domain.Shapes;
using System;

namespace GeoMaster.Api.Services.Contencao
{
    public class RetanguloEmCirculoStrategy : IContencaoStrategy
    {
        public bool Supports(Type externa, Type interna) => externa == typeof(Circulo) && interna == typeof(Retangulo);

        public bool Contida(object externa, object interna)
        {
            var c = (Circulo)externa;
            var r = (Retangulo)interna;
            var diagonal = Math.Sqrt(r.Largura * r.Largura + r.Altura * r.Altura);
            return diagonal <= 2 * c.Raio;
        }
    }
}
