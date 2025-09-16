using GeoMaster.Api.Domain.Shapes;
using System;

namespace GeoMaster.Api.Services.Contencao
{
    public class RetanguloEmRetanguloStrategy : IContencaoStrategy
    {
        public bool Supports(Type externa, Type interna) => externa == typeof(Retangulo) && interna == typeof(Retangulo);

        public bool Contida(object externa, object interna)
        {
            var ext = (Retangulo)externa;
            var inn = (Retangulo)interna;
            return inn.Largura <= ext.Largura && inn.Altura <= ext.Altura;
        }
    }
}
