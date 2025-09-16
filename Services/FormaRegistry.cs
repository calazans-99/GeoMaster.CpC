using GeoMaster.Api.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace GeoMaster.Api.Services
{
    public interface IFormaRegistry
    {
        void Register(string key, Type dtoType, Func<object, object> mapToDomain);
        bool TryGet(string key, out (Type DtoType, Func<object, object> Map) entry);
    }

    public class FormaRegistry : IFormaRegistry
    {
        private readonly Dictionary<string, (Type DtoType, Func<object, object> Map)> _map =
            new(StringComparer.OrdinalIgnoreCase);

        public void Register(string key, Type dtoType, Func<object, object> mapToDomain)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            _map[key] = (dtoType, mapToDomain);
        }

        public bool TryGet(string key, out (Type DtoType, Func<object, object> Map) entry) =>
            _map.TryGetValue(key, out entry);
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddShape<TDto, TDomain>(this IServiceCollection services, string key, Func<TDto, TDomain> mapper)
        {
            // registra no startup (após o container estar pronto)
            services.AddSingleton<IStartupFilter>(new FormaRegistryStartupFilter(
                key, typeof(TDto), dto => mapper((TDto)dto)
            ));
            return services;
        }

        private sealed class FormaRegistryStartupFilter : IStartupFilter
        {
            private readonly string _key;
            private readonly Type _dtoType;
            private readonly Func<object, object> _map;

            public FormaRegistryStartupFilter(string key, Type dtoType, Func<object, object> map)
            {
                _key = key; _dtoType = dtoType; _map = map;
            }

            public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            {
                return app =>
                {
                    var registry = app.ApplicationServices.GetRequiredService<IFormaRegistry>();
                    registry.Register(_key, _dtoType, _map);
                    next(app);
                };
            }
        }
    }

    public interface IFormaFactory
    {
        object? CriarForma(ShapeInputDto input);
    }

    public class FormaFactory : IFormaFactory
    {
        private readonly IFormaRegistry _registry;

        public FormaFactory(IFormaRegistry registry) { _registry = registry; }

        public object? CriarForma(ShapeInputDto input)
        {
            if (input?.Tipo is null) return null;
            if (!_registry.TryGet(input.Tipo, out var entry)) return null;

            var dtoObj = input.Parametros.Deserialize(entry.DtoType, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (dtoObj is null) return null;
            return entry.Map(dtoObj);
        }
    }
}
