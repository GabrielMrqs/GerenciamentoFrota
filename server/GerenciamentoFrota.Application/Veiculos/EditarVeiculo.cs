using FluentValidation;
using GerenciamentoFrota.Domain.Veiculos;
using GerenciamentoFrota.Domain.Shared;
using GerenciamentoFrota.Infra.Veiculos;
using MediatR;

namespace GerenciamentoFrota.Application.Veiculos
{
    public class EditarVeiculoHandler : IRequestHandler<EditarVeiculoCommand, Result<Exception, Unit>>
    {
        private readonly IVeiculoService _service;

        public EditarVeiculoHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<Result<Exception, Unit>> Handle(EditarVeiculoCommand request, CancellationToken cancellationToken)
        {
            var veiculo = request.Veiculo;

            var jaExisteChassi = await _service.ExisteVeiculoPeloChassi(veiculo.Chassi);

            if (!jaExisteChassi)
            {
                return new Exception($"Não foi encontrado veículo com o chassi: {veiculo.Chassi}");
            }

            await _service.EditarVeiculo(veiculo);

            return Unit.Task.Result;
        }
    }

    public record EditarVeiculoCommand(Veiculo Veiculo) : IRequest<Result<Exception, Unit>>;

    public class EditarVeiculoCommandValidator : AbstractValidator<EditarVeiculoCommand>
    {
        public EditarVeiculoCommandValidator()
        {
            RuleFor(x => x.Veiculo.Chassi)
                .NotEmpty().WithMessage("Informe o chassi")
                .Length(17).WithMessage("O chassi deve ter 17 caracteres");

            RuleFor(x => x.Veiculo.NumeroPassageiros)
                .Must(x => x == 2)
                .When(x => x.Veiculo.TipoVeiculo == TipoVeiculo.Caminhao)
                .WithMessage("Caminhões só podem ter 2 passageiros");

            RuleFor(x => x.Veiculo.NumeroPassageiros)
                .Must(x => x == 42)
                .When(x => x.Veiculo.TipoVeiculo == TipoVeiculo.Onibus)
                .WithMessage("Ônibus só podem ter 42 passageiros");

            RuleFor(x => x.Veiculo.Cor)
                .NotEmpty().WithMessage("Informe a cor");
        }
    }
}
