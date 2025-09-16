using GeoMaster.Api.Domain.Shapes;
using System;

namespace GeoMaster.Api.Services.Contencao
{
    public class CirculoEmCirculoStrategy : IContencaoStrategy
    {
        public bool Supports(Type externa, Type interna) => externa == typeof(Circulo) && interna == typeof(Circulo);

        public bool Contida(object externa, object interna)
        {
            var ext = (Circulo)externa;
            var inn = (Circulo)interna;
            return inn.Raio <= ext.Raio;
        }
    }
}
