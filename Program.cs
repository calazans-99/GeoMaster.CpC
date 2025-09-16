using GeoMaster.Api.Services;
using GeoMaster.Api.Services.Contencao;
using Microsoft.OpenApi.Models;

// aliases necessários
using DTOs = GeoMaster.Api.DTOs;
using DomainShapes = GeoMaster.Api.Domain.Shapes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GeoMaster API",
        Version = "v1",
        Description = "API de cálculos geométricos (2D e 3D)"
    });
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
});

builder.Services.AddControllers();

// Core services
builder.Services.AddSingleton<IFormaRegistry, FormaRegistry>();
builder.Services.AddSingleton<IFormaFactory, FormaFactory>();
builder.Services.AddSingleton<ICalculadoraService, CalculadoraService>();

// Registrar formas (extensível)
builder.Services.AddShape<DTOs.CirculoDto, DomainShapes.Circulo>("circulo", d => new DomainShapes.Circulo(d.Raio));
builder.Services.AddShape<DTOs.RetanguloDto, DomainShapes.Retangulo>("retangulo", d => new DomainShapes.Retangulo(d.Largura, d.Altura));
builder.Services.AddShape<DTOs.EsferaDto, DomainShapes.Esfera>("esfera", d => new DomainShapes.Esfera(d.Raio));

// Strategies de contenção (Desafio final)
builder.Services.AddSingleton<IContencaoStrategy, CirculoEmRetanguloStrategy>();
builder.Services.AddSingleton<IContencaoStrategy, RetanguloEmCirculoStrategy>();
builder.Services.AddSingleton<IContencaoStrategy, CirculoEmCirculoStrategy>();
builder.Services.AddSingleton<IContencaoStrategy, RetanguloEmRetanguloStrategy>();
builder.Services.AddSingleton<IContencaoStrategy, EsferaEmEsferaStrategy>();
builder.Services.AddSingleton<IContencaoResolver, ContencaoResolver>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
