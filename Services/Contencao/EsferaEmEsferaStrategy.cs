using GeoMaster.Api.Domain.Shapes;
using System;

namespace GeoMaster.Api.Services.Contencao
{
    public class EsferaEmEsferaStrategy : IContencaoStrategy
    {
        public bool Supports(Type externa, Type interna) => externa == typeof(Esfera) && interna == typeof(Esfera);

        public bool Contida(object externa, object interna)
        {
            var ext = (Esfera)externa;
            var inn = (Esfera)interna;
            return inn.Raio <= ext.Raio;
        }
    }
}
