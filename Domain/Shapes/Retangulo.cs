using GeoMaster.Api.Domain.Interfaces;
using System;

namespace GeoMaster.Api.Domain.Shapes
{
    public class Retangulo : ICalculos2D
    {
        public double Largura { get; }
        public double Altura { get; }

        public Retangulo(double largura, double altura)
        {
            if (largura <= 0) throw new ArgumentOutOfRangeException(nameof(largura));
            if (altura <= 0) throw new ArgumentOutOfRangeException(nameof(altura));
            Largura = largura;
            Altura = altura;
        }

        public double CalcularArea() => Largura * Altura;
        public double CalcularPerimetro() => 2 * (Largura + Altura);
    }
}
