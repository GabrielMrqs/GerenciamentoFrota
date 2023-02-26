using FluentValidation;
using GerenciamentoFrota.Application.Extensions;
using GerenciamentoFrota.Application.Veiculos;
using GerenciamentoFrota.Infra.Shared;
using GerenciamentoFrota.Infra.Veiculos;
using MediatR;
using MongoDB.ApplicationInsights.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(config =>
{
    config.AddPolicy(name: "GerenciamentoFrota", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.RegisterRequestHandlers();

builder.Services.RegisterValidationHandlers();

builder.Services.AddMongoClient("mongodb://usr:pwd@host.docker.internal:27017/?authMechanism=SCRAM-SHA-256");

builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = "host.docker.internal";
    config.InstanceName = "Frota";
});

builder.Services.AddScoped<VeiculoService>();

builder.Services.AddScoped<VeiculoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("GerenciamentoFrota");

app.UseHttpsRedirection();

app.MapPost("/InserirVeiculo", async (IMediator mediator, IValidator<InserirVeiculoCommand> validator, InserirVeiculoCommand inserirVeiculoCommand) =>
{
    var validation = await validator.ValidateAsync(inserirVeiculoCommand);

    if (!validation.IsValid)
    {
        return Results.BadRequest(validation.Errors.ErrorsOnly());
    }

    var callback = await mediator.Send(inserirVeiculoCommand);

    if (callback.IsFailure)
    {
        return Results.BadRequest(callback.Failure.Message);
    }

    return Results.Ok(callback.Success);
})
.WithName("Inserir veículo");

app.MapPut("/EditarVeiculo", async (IMediator mediator, IValidator<EditarVeiculoCommand> validator, EditarVeiculoCommand editarVeiculoCommand) =>
{
    var validation = await validator.ValidateAsync(editarVeiculoCommand);

    if (!validation.IsValid)
    {
        return Results.BadRequest(validation.Errors.ErrorsOnly());
    }

    var callback = await mediator.Send(editarVeiculoCommand);

    if (callback.IsFailure)
    {
        return Results.BadRequest(callback.Failure.Message);
    }

    return Results.Ok(callback.Success);
})
.WithName("Editar veículo");

app.MapDelete("/ExcluirVeiculo", async (IMediator mediator, string chassi) =>
{
    var callback = await mediator.Send(new ExcluirVeiculoCommand(chassi));

    if (callback.IsFailure)
    {
        return Results.BadRequest(callback.Failure.Message);
    }

    return Results.Ok(callback.Success);
})
.WithName("Excluir veículo");

app.MapGet("/VisualizarVeiculoPorChassi",
    async (IMediator mediator, IValidator<VisualizarVeiculoCommandPorChassi> validator, string chassi) =>
{
    var visualizarVeiculoCommand = new VisualizarVeiculoCommandPorChassi(chassi);

    var validation = await validator.ValidateAsync(visualizarVeiculoCommand);

    if (!validation.IsValid)
    {
        return Results.BadRequest(validation.Errors.ErrorsOnly());
    }

    var callback = await mediator.Send(visualizarVeiculoCommand);

    if (callback.IsFailure)
    {
        return Results.BadRequest(callback.Failure.Message);
    }

    return Results.Ok(callback.Success);
})
.WithName("Visualizar veículo por chassi");

app.MapGet("/VisualizarTodosVeiculos", async (IMediator mediator) =>
{
    var callback = await mediator.Send(new VisualizarTodosVeiculosCommand());

    if (callback.IsFailure)
    {
        return Results.BadRequest(callback.Failure.Message);
    }

    return Results.Ok(callback.Success);
})
.WithName("Visualizar todos os veículos");

app.Run();