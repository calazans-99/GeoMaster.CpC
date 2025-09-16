using GeoMaster.Api.Domain.Shapes;
using System;

namespace GeoMaster.Api.Services.Contencao
{
    public class CirculoEmRetanguloStrategy : IContencaoStrategy
    {
        public bool Supports(Type externa, Type interna) => externa == typeof(Retangulo) && interna == typeof(Circulo);

        public bool Contida(object externa, object interna)
        {
            var r = (Retangulo)externa;
            var c = (Circulo)interna;
            return 2 * c.Raio <= r.Largura && 2 * c.Raio <= r.Altura;
        }
    }
}
