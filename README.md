# NuCapitalGains

## 📌 Descrição

NuCapitalGains é um aplicativo de linha de comando que processa operações de compra e venda de ações, calculando o imposto devido com base nas regras de ganho de capital.

## 🏗 **Arquitetura do Projeto**

A arquitetura do **NuCapitalGains** segue os princípios da **Clean Architecture**, organizando o código de forma modular e garantindo **baixo acoplamento e alta coesão**. Essa abordagem facilita **testabilidade, manutenção e escalabilidade** do sistema.

### ✅ **Camadas do Projeto**

| Camada                         | Responsabilidade                                                                          |
| ------------------------------ | ----------------------------------------------------------------------------------------- |
| **NuCapitalGains.Application** | Interface do usuário (CLI), processa entrada e exibe saída.                               |
| **NuCapitalGains.Core**        | Contém **regras de negócio** e **contratos (interfaces)** para comunicação entre camadas. |
| **NuCapitalGains.Infra**       | Implementação dos serviços e infraestrutura.                                              |
| **NuCapitalGains.Tests**       | Testes unitários e de integração para garantir a qualidade do sistema.                    |

### 🔹 **Fluxo das Dependências**

- As camadas externas (**Application** e **Infra**) dependem da camada central (**Core**), mas nunca o contrário.
- A camada **Core** define contratos (**interfaces**), que são implementados pela camada **Infra**.

## 🔎 **Explicação das Camadas**

### **1️⃣ Camada `Core` (Regras de Negócio e Contratos)**

O `Core` é a camada mais importante porque contém:

- **Entidades** representando os modelos de domínio.
- **Regras de negócio puras** (sem dependências externas).
- **Interfaces para serviços**, permitindo que outras camadas implementem funcionalidades sem acoplamento direto.

#### Exemplo de Interface no Core (`ICalculatorService.cs`)

```csharp
public interface ICalculatorService
{
    OperationResult ProcessOperation(Operation operation);
}
```

### **2️⃣ Camada `Infra` (Implementação de Serviços)**

A camada `Infra` implementa os serviços definidos no `Core`, evitando acoplamento direto.

- Conexões externas, como **banco de dados** e **APIs externas**.
- Implementação dos contratos definidos no `Core`.

#### Exemplo de Implementação no Infra

```csharp
public class CalculatorService : ICalculatorService
{
    public OperationResult ProcessOperation(Operation operation)
    {
        // Implementação da lógica de cálculo de imposto.
    }
}
```

### **3️⃣ Camada `Application` (CLI e Interface do Usuário)**

Aqui está a interação com o usuário, lendo entrada e exibindo saída.

**Essa camada não contém regras de negócio**, apenas chama os serviços do `Core`.

#### Exemplo de Execução do CLI

```csharp
public static void Main()
{
    using StreamReader reader = new(Console.OpenStandardInput());
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        var result = Process(line);
        Console.WriteLine(result);
    }
}
```

### **4️⃣ Camada `Tests` (Testes Unitários e de Integração)**

- **Testes unitários** verificam métodos individuais no `Core`.
- **Testes de integração** rodam a aplicação inteira e validam a saída esperada.

## 📂 **Estrutura do Projeto**

```
nu-capital-gains/
│── src/
│   ├── NuCapitalGains.Application/  # Aplicação Console
│   ├── NuCapitalGains.Core/         # Regras de negócio
│   ├── NuCapitalGains.Infra/        # Serviços de infraestrutura
│   ├── NuCapitalGains.Tests/        # Testes unitários e de integração
│── input.txt                        # Exemplo de entrada
│── README.md                        # Documentação
```

---

## 📌 **Pré-requisitos**

Antes de rodar o projeto, certifique-se de ter instalado:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

---

## ▶️ **Executando o Aplicativo no Console**

### 🔹 **Rodando com Entrada Manual**

```sh
dotnet run --project src/NuCapitalGains.Application
```

Agora você pode inserir operações manualmente:

```json
[{ "operation": "buy", "quantity": 100, "unit-cost": 10.0 }]
```

**Saída esperada:**

```json
[{ "tax": 0 }]
```

### 🔹 **Rodando com Entrada via Arquivo (`stdin`)**

```sh
dotnet run --project src/NuCapitalGains.Application < input.txt
```

### 🔹 **Rodando e Salvando a Saída (`stdout`)**

```sh
dotnet run --project src/NuCapitalGains.Application < input.txt > output.txt
```

## 🧪 **Rodando os Testes**

```sh
dotnet test src/NuCapitalGains.Tests
```

Para mais detalhes:

```sh
dotnet test src/NuCapitalGains.Tests --logger "console;verbosity=detailed"
```

Agora seu projeto segue boas práticas e está bem documentado!
