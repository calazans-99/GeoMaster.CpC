using GeoMaster.Api.Domain.Interfaces;
using System;

namespace GeoMaster.Api.Services
{
    public interface ICalculadoraService
    {
        double CalcularArea(object forma);
        double CalcularPerimetro(object forma);
        double CalcularVolume(object forma);
        double CalcularAreaSuperficial(object forma);
    }

    public class CalculadoraService : ICalculadoraService
    {
        public double CalcularArea(object forma) =>
            forma is ICalculos2D f2d ? f2d.CalcularArea()
            : throw new InvalidOperationException("A forma fornecida não suporta área (2D).");

        public double CalcularPerimetro(object forma) =>
            forma is ICalculos2D f2d ? f2d.CalcularPerimetro()
            : throw new InvalidOperationException("A forma fornecida não suporta perímetro (2D).");

        public double CalcularVolume(object forma) =>
            forma is ICalculos3D f3d ? f3d.CalcularVolume()
            : throw new InvalidOperationException("A forma fornecida não suporta volume (3D).");

        public double CalcularAreaSuperficial(object forma) =>
            forma is ICalculos3D f3d ? f3d.CalcularAreaSuperficial()
            : throw new InvalidOperationException("A forma fornecida não suporta área superficial (3D).");
    }
}
