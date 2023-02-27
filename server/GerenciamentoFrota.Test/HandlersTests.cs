using FluentAssertions;
using GerenciamentoFrota.Application.Veiculos;
using GerenciamentoFrota.Domain.Veiculos;
using GerenciamentoFrota.Infra.Veiculos;
using Moq;

namespace GerenciamentoFrota.Test
{
    public class HandlersTests
    {
        private readonly Mock<IVeiculoService> _veiculoServiceMock;
        private readonly Mock<Veiculo> _veiculo;

        public HandlersTests()
        {
            _veiculoServiceMock = new();
            _veiculo = new();
        }

        [Fact]
        public async Task DeveInserirVeiculo()
        {
            var command = new InserirVeiculoCommand(_veiculo.Object);

            _veiculoServiceMock.Setup(x =>
            x.ExisteVeiculoPeloChassi(It.IsAny<string>())).Returns(Task.FromResult(false));

            var handler = new InserirVeiculoHandler(_veiculoServiceMock.Object);

            var result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();

            result.IsFailure.Should().BeFalse();
        }

        [Fact]
        public async Task NaoDeveInserirVeiculo()
        {
            var chassi = "12345678901234567";

            _veiculo.SetupProperty(x => x.Chassi, chassi);

            var command = new InserirVeiculoCommand(_veiculo.Object);

            _veiculoServiceMock.Setup(x =>
            x.ExisteVeiculoPeloChassi(It.IsAny<string>())).Returns(Task.FromResult(true));

            var handler = new InserirVeiculoHandler(_veiculoServiceMock.Object);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();

            result.Failure.Message.Should().Be($"Já existe veículo com o chassi: {chassi}");

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task DeveEditarVeiculo()
        {
            var command = new EditarVeiculoCommand(_veiculo.Object);

            _veiculoServiceMock.Setup(x =>
            x.ExisteVeiculoPeloChassi(It.IsAny<string>())).Returns(Task.FromResult(true));

            var handler = new EditarVeiculoHandler(_veiculoServiceMock.Object);

            var result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();

            result.IsFailure.Should().BeFalse();
        }

        [Fact]
        public async Task NaoDeveEditarVeiculo()
        {
            var chassi = "12345678901234567";

            _veiculo.SetupProperty(x => x.Chassi, chassi);

            var command = new EditarVeiculoCommand(_veiculo.Object);

            _veiculoServiceMock.Setup(x =>
            x.ExisteVeiculoPeloChassi(It.IsAny<string>())).Returns(Task.FromResult(false));

            var handler = new EditarVeiculoHandler(_veiculoServiceMock.Object);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();

            result.Failure.Message.Should().Be($"Não foi encontrado veículo com o chassi: {chassi}");

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task DeveExcluirVeiculo()
        {
            var chassi = "12345678901234567";

            var command = new ExcluirVeiculoCommand(chassi);

            _veiculoServiceMock.Setup(x =>
            x.ExisteVeiculoPeloChassi(It.IsAny<string>())).Returns(Task.FromResult(true));

            var handler = new ExcluirVeiculoHandler(_veiculoServiceMock.Object);

            var result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();

            result.IsFailure.Should().BeFalse();
        }

        [Fact]
        public async Task NaoDeveExcluirVeiculo()
        {
            var chassi = "12345678901234567";

            var command = new ExcluirVeiculoCommand(chassi);

            _veiculoServiceMock.Setup(x =>
            x.ExisteVeiculoPeloChassi(It.IsAny<string>())).Returns(Task.FromResult(false));

            var handler = new ExcluirVeiculoHandler(_veiculoServiceMock.Object);

            var result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeFalse();

            result.Failure.Message.Should().Be($"Não foi encontrado veículo com o chassi: {chassi}");

            result.IsFailure.Should().BeTrue();
        }
    }
}