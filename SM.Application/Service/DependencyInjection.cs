using Microsoft.Extensions.DependencyInjection;
using SM.Application.Interfaces;
using SM.Application.Service;
using SM.Domaiin.Interfaces;
using SM.Infra.Repositories;
using System.Text.Json.Serialization;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<ClienteRepository>();
        services.AddScoped<EnderecoRepository>();
        services.AddScoped<TecnicoRepository>();
        services.AddScoped<ServicosRepository>();
        services.AddScoped<EnderecoComplementoRepository>();


        // Interfaces
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<ITecnicoRepository, TecnicoRepository>();
        services.AddScoped<IServicosRepository, ServicosRepository>();
        services.AddScoped<IEnderecoComplementoRepository, EnderecoComplementoRepository>();

        // Services (Registrar as dependências primeiro)
        services.AddScoped<EnderecoComplementoService>();
        services.AddScoped<EnderecoService>();
        services.AddScoped<IEnderecoService, EnderecoService>();  // EnderecoService antes
        services.AddScoped<IEnderecoComplementoService, EnderecoComplementoService>();  // EnderecoComplementoService antes
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Serviços que dependem das dependências acima
        services.AddScoped<IClienteService, ClienteService>();  // ClienteService que depende de EnderecoComplementoService
        services.AddScoped<ITecnicoService, TecnicoService>();  // TecnicoService que depende de EnderecoService
        services.AddScoped<IServicosService, ServicosService>();
        services.AddScoped<TecnicoService>();

        // Controllers & API Setup
        services.AddControllers().AddJsonOptions(options => 
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddEndpointsApiExplorer();

        // Outros serviços Singleton
        services.AddSingleton<RabbitMQProducer>();

        return services;
    }
}
