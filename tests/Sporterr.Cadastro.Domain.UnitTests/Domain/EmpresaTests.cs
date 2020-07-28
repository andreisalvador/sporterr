using FluentAssertions;
using FluentValidation;
using Sporterr.Cadastro.Domain;
using Sporterr.Cadastro.UnitTests.Fixtures;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.Enums;
using System;
using System.Linq;
using Xunit;

namespace Sporterr.Cadastro.UnitTests.Domain
{

    [Collection(nameof(FixtureWrapper))]
    public class EmpresaTests
    {
        private readonly FixtureWrapper _fixtureWrapper;
        public EmpresaTests(FixtureWrapper fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
        }

        [Fact(DisplayName = "Nova empresa inválida")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_Validate_EmpresaDeveSerInvalida()
        {
            //Arrange & Act & Assert
            _fixtureWrapper.Empresa.Invoking(x => x.CriarEmpresaInvalida()).Should().Throw<ValidationException>();
        }

        [Fact(DisplayName = "Nova empresa válida")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_Validate_EmpresaDeveSerValida()
        {
            //Arrange & Act
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();

            //Assert
            empresa.Should().NotBeNull();
            empresa.Ativo.Should().BeTrue();
        }

        [Fact(DisplayName = "Adicionar nova quadra na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AdicionarQuadra_DeveAdicionarQuadraComSucesso()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();

            //Act
            empresa.AdicionarQuadra(quadra);

            //Arrange
            empresa.Quadras.Should().HaveCount(1);
            quadra.Should().BeSameAs(empresa.Quadras.SingleOrDefault());
        }

        [Fact(DisplayName = "Adicionar quadra já existente na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AdicionarQuadra_DeveFalharPoisQuadraJaExisteNaEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();
            empresa.AdicionarQuadra(quadra);

            //Act & Arrange
            empresa.Invoking(x => x.AdicionarQuadra(quadra)).Should().Throw<DomainException>();
        }

        [Fact(DisplayName = "Inativar quadra da empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_InativarQuadra_DeveInativarQuadraComSucesso()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();
            quadra.Empresa = empresa;
            empresa.AdicionarQuadra(quadra);

            //Act
            empresa.InativarQuadra(quadra);

            //Assert
            quadra.Ativo.Should().BeFalse();
        }

        [Fact(DisplayName = "Inativar quadra já inativa da empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_InativarQuadra_DeveFalharPoisQuadraJaEstaInativa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();
            quadra.Empresa = empresa;
            quadra.Inativar();

            //Act & Assert
            empresa.Invoking(x => x.InativarQuadra(quadra)).Should().Throw<DomainException>();
        }

        [Fact(DisplayName = "Inativar quadra não pertencente a empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_InativarQuadra_DeveFalharPoisQuadraNaoPertenceEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();

            //Act & Assert
            empresa.Invoking(x => x.InativarQuadra(quadra)).Should().Throw<DomainException>();
        }

        [Fact(DisplayName = "Reativar quadra da empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_ReativarQuadra_DeveReativarQuadraComSucesso()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();
            quadra.Empresa = empresa;
            quadra.Inativar();
            empresa.AdicionarQuadra(quadra);

            //Act
            empresa.ReativarQuadra(quadra);

            //Assert
            quadra.Ativo.Should().BeTrue();
        }

        [Fact(DisplayName = "Reativar quadra já ativa da empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_ReativarQuadra_DeveFalharPoisQuadraJaEstaAtiva()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();

            //Act & Assert
            empresa.Invoking(x => x.ReativarQuadra(quadra)).Should().Throw<DomainException>();
        }

        [Fact(DisplayName = "Reativar quadra não pertencente a empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_ReativarQuadra_DeveFalharPoisQuadraNaoPertenceEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();
            quadra.Inativar();

            //Act & Assert
            empresa.Invoking(x => x.ReativarQuadra(quadra)).Should().Throw<DomainException>();
        }

        [Fact(DisplayName = "Coloca quadra em manutenção na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_ColocarQuadraEmManutencao_DeveColocarQuadraEmEstadoDeManutencao()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();
            quadra.Empresa = empresa;
            empresa.AdicionarQuadra(quadra);

            //Act
            empresa.ColocarQuadraEmManutencao(quadra);

            //Assert
            quadra.EmManutencao.Should().BeTrue();
        }

        [Fact(DisplayName = "Coloca quadra que já esta em manutenção, em manutenção na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_ColocarQuadraEmManutencao_DeveFalharPoisQuadraJaEstaEmManutencao()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();

            //Act & Assert
            empresa.Invoking(x => x.ColocarQuadraEmManutencao(quadra)).Should().Throw<DomainException>();
        }


        [Fact(DisplayName = "Retira quadra de manutenção na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_RetirarQuadraDeManutencao_DeveRetirarQuadraDoEstadoDeManutencao()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();
            quadra.Empresa = empresa;
            empresa.AdicionarQuadra(quadra);
            empresa.ColocarQuadraEmManutencao(quadra);

            //Act
            empresa.RetirarQuadraDeManutencao(quadra);

            //Assert
            quadra.EmManutencao.Should().BeFalse();
        }

        [Fact(DisplayName = "Retira quadra que não esta em manutenção da manutenção na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_RetirarQuadraDeManutencao_DeveFalharPoisQuadraNaoEstaEmManutencao()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();

            //Act & Assert
            empresa.Invoking(x => x.RetirarQuadraDeManutencao(quadra)).Should().Throw<DomainException>();
        }



        [Fact(DisplayName = "Altera horário de abertura da empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AlterarHorarioAbertura_DeveAlterarHorarioDeAbertura()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            TimeSpan novoHorarioAbertura = TimeSpan.FromHours(9);

            //Act
            empresa.AlterarHorarioAbertura(novoHorarioAbertura);

            //Assert
            novoHorarioAbertura.Should().Be(empresa.HorarioAbertura);
        }

        [Fact(DisplayName = "Altera horário de fechamento da empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AlterarHorarioAbertura_DeveAlterarHorarioDeFechamento()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            TimeSpan novoHorarioFechamento = TimeSpan.FromHours(17);

            //Act
            empresa.AlterarHorarioFechamento(novoHorarioFechamento);

            //Assert
            novoHorarioFechamento.Should().Be(empresa.HorarioFechamento);
        }

        [Fact(DisplayName = "Altera horário de funcionamento da empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AlterarHorarioAbertura_DeveAlterarHorarioDeAberturaFechamentoDaEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            TimeSpan novoHorarioAbertura = TimeSpan.FromHours(9);
            TimeSpan novoHorarioFechamento = TimeSpan.FromHours(17);

            //Act
            empresa.AlterarHorarioFuncionamento(novoHorarioAbertura, novoHorarioFechamento);

            //Assert
            novoHorarioAbertura.Should().Be(empresa.HorarioAbertura);
            novoHorarioFechamento.Should().Be(empresa.HorarioFechamento);
        }

        [Fact(DisplayName = "Adicionar um novo dia de funcionamento a empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AtivarFuncionamentoNoDiaDaSemana_DeveAdicionarUmNovoDiaDeFuncionamento()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            DiasSemanaFuncionamento diasSemanaFuncionamentoAtual = empresa.DiasFuncionamento;

            //Act
            empresa.AtivarFuncionamentoNoDiaDaSemana(Core.Enums.DiasSemanaFuncionamento.Sabado);

            //Assert
            empresa.DiasFuncionamento.Should().Be((int)diasSemanaFuncionamentoAtual + DiasSemanaFuncionamento.Sabado);
        }

        [Fact(DisplayName = "Desativar um dia de funcionamento a empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_DesativarFuncionamentoNoDiaDaSemana_DeveRemoverUmDiaDeFuncionamento()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            DiasSemanaFuncionamento diasSemanaFuncionamentoAtual = empresa.DiasFuncionamento;

            //Act
            empresa.DesativarFuncionamentoNoDiaDaSemana(Core.Enums.DiasSemanaFuncionamento.Quarta);

            //Assert
            empresa.DiasFuncionamento.Should().Be((int)diasSemanaFuncionamentoAtual - DiasSemanaFuncionamento.Quarta);
        }

        [Fact(DisplayName = "Alterar dias de funcionamento a empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AlterarDiasFuncionamento_DeveRemoverUmDiaDeFuncionamento()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();

            //Act
            empresa.AlterarDiasFuncionamento(DiasSemanaFuncionamento.Todos);

            //Assert
            empresa.DiasFuncionamento.Should().Be(DiasSemanaFuncionamento.Todos);
        }

        [Fact(DisplayName = "Inativa a empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_Inativar_DeveInativarEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();

            //Act
            empresa.Inativar();

            //Assert
            empresa.Ativo.Should().BeFalse();
        }
    }
}
