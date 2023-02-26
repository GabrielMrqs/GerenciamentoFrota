using FluentValidation;
using GerenciamentoFrota.Application.Extensions;
using GerenciamentoFrota.Application.Veiculos;
using GerenciamentoFrota.Infra.Shared.Extensions;
using GerenciamentoFrota.Infra.Veiculos;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVeiculoService, VeiculoService>();

builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();

builder.RegisterRequestHandlers();

builder.RegisterValidationHandlers();

builder.ConfigureRedis();

builder.ConfigureMongoClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
});

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