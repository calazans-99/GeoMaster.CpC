using System;
using System.Collections.Generic;

namespace GeoMaster.Api.Services.Contencao
{
    public interface IContencaoStrategy
    {
        bool Supports(Type externa, Type interna);
        bool Contida(object externa, object interna);
    }

    public interface IContencaoResolver
    {
        IContencaoStrategy? Resolve(Type externa, Type interna);
    }

    public class ContencaoResolver : IContencaoResolver
    {
        private readonly IEnumerable<IContencaoStrategy> _strategies;
        public ContencaoResolver(IEnumerable<IContencaoStrategy> strategies) => _strategies = strategies;

        public IContencaoStrategy? Resolve(Type externa, Type interna) =>
            _strategies.FirstOrDefault(s => s.Supports(externa, interna));
    }
}
