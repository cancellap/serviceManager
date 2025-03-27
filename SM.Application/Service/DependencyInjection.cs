using Microsoft.Extensions.DependencyInjection;
using SM.Application.Interfaces;
using SM.Application.Service;
using SM.Domaiin.Interfaces;
using SM.Infra.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ClienteRepository>();
        services.AddScoped<EnderecoRepository>();
        services.AddScoped<IEnderecoService, EnderecoService>();
        services.AddScoped<TecnicoRepository>();
        services.AddScoped<ServicosRepository>();
        services.AddScoped<IServicosRepository, ServicosRepository>();
        services.AddScoped<EnderecoComplementoRepository>();
        services.AddScoped<IEnderecoComplementoRepository, EnderecoComplementoRepository>();
        services.AddScoped<ITecnicoRepository, TecnicoRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();
        services.AddScoped<ClienteService>();
        services.AddScoped<EnderecoService>();
        services.AddScoped<TecnicoService>();
        services.AddScoped<ServicosService>();
        services.AddScoped<ITecnicoService, TecnicoService>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSingleton<RabbitMQProducer>();


        return services;
    }
}
