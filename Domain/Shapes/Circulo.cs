using GeoMaster.Api.Domain.Interfaces;
using System;

namespace GeoMaster.Api.Domain.Shapes
{
    public class Circulo : ICalculos2D
    {
        public double Raio { get; }

        public Circulo(double raio)
        {
            if (raio <= 0) throw new ArgumentOutOfRangeException(nameof(raio));
            Raio = raio;
        }

        public double CalcularArea() => Math.PI * Raio * Raio;
        public double CalcularPerimetro() => 2 * Math.PI * Raio;
    }
}
