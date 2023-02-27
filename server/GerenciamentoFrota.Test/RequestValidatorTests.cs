using FluentAssertions;
using GerenciamentoFrota.Application.Veiculos;
using GerenciamentoFrota.Domain.Veiculos;
using GerenciamentoFrota.Infra.Veiculos;
using Moq;

namespace GerenciamentoFrota.Test
{
    public class RequestValidatorsTests
    {

        public RequestValidatorsTests()
        {

        }

        [Fact]
        public async Task DeveTrazer3ErrosInserirCommand()
        {
            var veiculo = new Veiculo
            {
                Chassi = "1234567890123456",
                Cor = "",
                NumeroPassageiros = 42,
                TipoVeiculo = TipoVeiculo.Caminhao
            };
            var command = new InserirVeiculoCommand(veiculo);

            var validator = new InserirVeiculoCommandValidator();

            var result = await validator.ValidateAsync(command);

            result.Errors.Count.Should().Be(3);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task DeveTrazer3ErrosEditarCommand()
        {
            var veiculo = new Veiculo
            {
                Chassi = "1234567890123456",
                Cor = "",
                NumeroPassageiros = 42,
                TipoVeiculo = TipoVeiculo.Caminhao
            };
            var command = new EditarVeiculoCommand(veiculo);

            var validator = new EditarVeiculoCommandValidator();

            var result = await validator.ValidateAsync(command);

            result.Errors.Count.Should().Be(3);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task DeveTrazer1ErroExcluirCommand()
        {
            var chassi = "123123123123123123";

            var command = new ExcluirVeiculoCommand(chassi);

            var validator = new ExcluirVeiculoCommandValidator();

            var result = await validator.ValidateAsync(command);

            result.Errors.Count.Should().Be(1);

            result.IsValid.Should().BeFalse();
        }        

        [Fact]
        public async Task DeveTrazer1ErroVisualizarPeloChassiCommand()
        {
            var chassi = "123123123123123123";

            var command = new VisualizarVeiculoPorChassiCommand(chassi);

            var validator = new VisualizarVeiculoPorChassiCommandValidator();

            var result = await validator.ValidateAsync(command);

            result.Errors.Count.Should().Be(1);

            result.IsValid.Should().BeFalse();
        }
    }
}