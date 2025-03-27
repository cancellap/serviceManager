using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SM.Application.Interfaces;
using SM.Application.Mapping;
using SM.Application.Service;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;
using SM.Infra.Data;
using SM.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
         b => b.MigrationsAssembly("SM.Infra")));

builder.Services.AddApplicationServices();

builder.Services.AddAutoMapper(typeof(ClienteMappingProfile),
                      typeof(EnderecoComplementoMappingProfile),
                      typeof(EnderecoMappingProfile),
                      typeof(TecnicoMappingProfile),
                      typeof(ServicoMappingProfile));

builder.Services.AddSwaggerGen();
var producer = new RabbitMQProducer();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
