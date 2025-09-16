# 🌐 GeoMaster API (CP4) — API de Cálculos Geométricos

API **ASP.NET Core Web API (.NET 8)** para cálculos geométricos **2D/3D**, documentada com **Swagger/OpenAPI**, e com endpoint de **validação de contenção entre formas** (Desafio Final). Projeto estruturado com **SOLID** e focado em **extensibilidade** (OCP).

---

## 👥 Equipe
- **Marcus Vinicius de Souza Calazans** — RM **556620**
- **Felipe Nogueira Ramon** — RM **555335**

> Dica: adicione aqui o **link do repositório GitHub** que será entregue.

---

## 🧰 Pré‑requisitos
- **.NET SDK 8.0+** (funciona também em .NET 9 trocando o `TargetFramework` para `net9.0`)
- (opcional) **Visual Studio 2022** ou **VS Code**

---

## ▶️ Como executar
```bash
dotnet restore
dotnet dev-certs https --trust   # apenas na 1ª vez em HTTPS
dotnet run --urls "http://localhost:5000;https://localhost:5001"
```
- Swagger UI: **https://localhost:5001/swagger** (ou **http://localhost:5000/swagger**)

> Problema de porta ocupada? Altere as portas no parâmetro `--urls` (ex.: `http://localhost:5080;https://localhost:5081`).

---

## 🧭 Endpoints principais (v1)

| Método | Rota                                   | Descrição                                   | Formas suportadas |
|:------:|----------------------------------------|---------------------------------------------|-------------------|
| POST   | `/api/v1/calculos/area`               | Calcula **área** (2D)                       | círculo, retângulo |
| POST   | `/api/v1/calculos/perimetro`          | Calcula **perímetro** (2D)                  | círculo, retângulo |
| POST   | `/api/v1/calculos/volume`             | Calcula **volume** (3D)                     | esfera            |
| POST   | `/api/v1/calculos/area-superficial`   | Calcula **área superficial** (3D)           | esfera            |
| POST   | `/api/v1/validacoes/forma-contida`    | Valida **contenção** (forma A contém B?)    | ver regras abaixo |

---

## 🧪 Exemplos de uso (Request/Response)

### 1) Área — **círculo** r=5
**POST** `/api/v1/calculos/area`
```json
{ "tipo": "circulo", "parametros": { "raio": 5 } }
```
**200 OK**
```json
{ "operacao": "area", "valor": 78.53981633974483 }
```

### 2) Perímetro — **retângulo** 10×4
**POST** `/api/v1/calculos/perimetro`
```json
{ "tipo": "retangulo", "parametros": { "largura": 10, "altura": 4 } }
```
**200 OK**
```json
{ "operacao": "perimetro", "valor": 28 }
```

### 3) Volume — **esfera** r=2.5
**POST** `/api/v1/calculos/volume`
```json
{ "tipo": "esfera", "parametros": { "raio": 2.5 } }
```
**200 OK**
```json
{ "operacao": "volume", "valor": 65.44984694978736 }
```

### 4) Área superficial — **esfera** r=5
**POST** `/api/v1/calculos/area-superficial`
```json
{ "tipo": "esfera", "parametros": { "raio": 5 } }
```
**200 OK**
```json
{ "operacao": "area_superficial", "valor": 314.1592653589793 }
```

### 5) Desafio Final — **contenção de formas**
**POST** `/api/v1/validacoes/forma-contida`
```json
{
  "formaExterna": { "tipo": "retangulo", "parametros": { "largura": 10, "altura": 10 } },
  "formaInterna": { "tipo": "circulo", "parametros": { "raio": 5 } }
}
```
**200 OK**
```json
{ "contida": true }
```

---

## 🧱 Regras de contenção suportadas
- **Círculo ∈ Retângulo**: `2r ≤ largura` **e** `2r ≤ altura`
- **Retângulo ∈ Círculo**: `√(w² + h²) ≤ 2R` (encaixe pela diagonal)
- **Círculo ∈ Círculo**: `r_in ≤ r_out`
- **Retângulo ∈ Retângulo**: `w_in ≤ w_out` **e** `h_in ≤ h_out` (sem rotação)
- **Esfera ∈ Esfera**: `r_in ≤ r_out`

> Observação: essa primeira versão **não considera rotação** do retângulo.

---

## ✅ Códigos de status (padrões)
- **200 OK** — cálculo/validação realizados com sucesso
- **400 Bad Request** — entrada inválida (ex.: `tipo` ausente/desconhecido, parâmetros inválidos, operação incompatível ou par de contenção não suportado)

Exemplo de erro (400):
```json
{
  "error": "Forma inválida ou não registrada."
}
```

---

## 🧩 Extensibilidade (OCP) — adicionando nova forma
1. **Crie um DTO** em `DTOs/` com validações (`[Range]`, `[Required]`)
2. **Implemente a classe de domínio** em `Domain/Shapes` que atenda `ICalculos2D` **ou** `ICalculos3D`
3. **Registre no `Program.cs`**:
   ```csharp
   builder.Services.AddShape<NovoDto, NovaForma>("nome", d => new NovaForma(/* mapeie do DTO */));
   ```
4. *(Opcional)* Para suportar **contenção** com outras formas, crie uma `IContencaoStrategy` em `Services/Contencao` e registre via DI

---

## 🗂️ Estrutura sugerida
```
GeoMaster.Api/
├─ Controllers/
├─ Domain/
│  ├─ Interfaces/ (ICalculos2D, ICalculos3D)
│  └─ Shapes/ (Circulo, Retangulo, Esfera)
├─ DTOs/
├─ Services/
│  ├─ Contencao/ (strategies + resolver)
│  ├─ ICalculadoraService.cs
│  └─ FormaRegistry.cs (registry + factory)
├─ Program.cs
└─ GeoMaster.Api.csproj
```

---

## 🧱 SOLID (onde aparece)
- **SRP**: cada classe possui uma responsabilidade (formas, cálculo, fábrica/registro, controllers, strategies)
- **ISP**: interfaces segregadas por **2D** e **3D** (`ICalculos2D` / `ICalculos3D`)
- **DIP**: controllers dependem de **abstrações** (`ICalculadoraService`, `IFormaFactory`) via DI
- **OCP**: adicionar novas formas/estratégias **sem** alterar o serviço principal (registro via `AddShape`)

---

## 🧪 Testes rápidos (cURL)
```bash
# Área (círculo r=5)
curl -X POST "https://localhost:5001/api/v1/calculos/area"   -H "Content-Type: application/json"   -d '{"tipo":"circulo","parametros":{"raio":5}}'
```

```bash
# Contenção (círculo r=5 em retângulo 10x10)
curl -X POST "https://localhost:5001/api/v1/validacoes/forma-contida"   -H "Content-Type: application/json"   -d '{"formaExterna":{"tipo":"retangulo","parametros":{"largura":10,"altura":10}},"formaInterna":{"tipo":"circulo","parametros":{"raio":5}}}'
```

---

## 📄 Licença
Uso acadêmico — ajuste conforme a necessidade da disciplina.
