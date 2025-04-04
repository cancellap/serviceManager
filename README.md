# ServiceManager

ServiceManager é uma aplicação .NET voltada para o gerenciamento de serviços técnicos, técnicos responsáveis e clientes, utilizando arquitetura em camadas e princípios DDD com abordagem hexagonal. O projeto utiliza Entity Framework Core para acesso a dados e RabbitMQ para comunicação assíncrona. Os testes são realizados com xUnit.

## 📦 Estrutura do Projeto

```
ServiceManager/ │
├── SM.Application/
  # Camada de aplicação (DTOs, services, interfaces)
├── SM.Domaiin/
  # Camada de domínio (entidades, validações, interfaces de repositórios)
├── SM.Infra/
  # Camada de infraestrutura (EF Core, repositórios)
├── ServiceManager/
  # API (controllers, configuração, startup)
├── SM.Workers/
  # Serviço worker para consumo do RabbitMQ
├── ServeceManageTests/
   # Projeto de testes com xUnit └── testeClassLibery/ # Projeto auxiliar de testes
```


## ⚙️ Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- **Entity Framework Core**
- **Dapper**
- **xUnit** para testes
- **RabbitMQ** para mensageria
- **AutoMapper** para mapeamento de objetos
- **Docker** e **docker-compose** para orquestração de serviços
- **PostgreSQL** como banco de dados relacional

✨ Funcionalidades
CRUD de Clientes, Técnicos e Serviços

Assincronia com RabbitMQ

Busca por filtros

Serviços desacoplados com uso de interfaces

Separação clara de responsabilidades com DDD

🧠 Arquitetura
DDD com domínio rico e validações

Arquitetura Hexagonal, isolando o domínio das implementações externas

Injeção de dependência configurada via DependencyInjection.cs

