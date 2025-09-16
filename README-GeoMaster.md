# GeoMaster API (CP4) — API de Cálculos Geométricos

Projeto ASP.NET Core Web API (.NET 8) para cálculos geométricos (2D e 3D) com documentação via Swagger e endpoint de validação de contenção de formas.

## Equipe
- Marcus Vinicius de Souza Calazans — RM 556620
- Felipe Nogueira Ramon — RM 555335
## Como executar
```bash
dotnet restore
dotnet dev-certs https --trust
dotnet run --urls "http://localhost:5000;https://localhost:5001"
```
Swagger: `https://localhost:5001/swagger` (ou `http://localhost:5000/swagger`).

## Endpoints principais (v1)
- `POST /api/v1/calculos/area`
Área — círculo r=5
{ "tipo": "circulo", "parametros": { "raio": 5 } }
- `POST /api/v1/calculos/perimetro`
Perímetro — retângulo 10x4
{ "tipo": "retangulo", "parametros": { "largura": 10, "altura": 4 } }
- `POST /api/v1/calculos/volume`
Volume — esfera r=2.5
{ "tipo": "esfera", "parametros": { "raio": 2.5 } }
- `POST /api/v1/calculos/area-superficial`
{
  "tipo": "esfera",
  "parametros": { "raio": 5 }
}
- `POST /api/v1/validacoes/forma-contida`

## Exemplo de requisição (JSON)
Área de um círculo (r = 5):
```json
{ "tipo": "circulo", "parametros": { "raio": 5 } }
```

## Observações
- Estrutura orientada a SOLID (SRP/ISP/DIP) e extensível (OCP) via registro de formas.
- Para adicionar nova forma: criar DTO + classe de domínio (2D/3D) e registrar em `Program.cs` com `AddShape`.
- Ajustar este README com o **link do repositório GitHub** usado na entrega quando estiver disponível.
