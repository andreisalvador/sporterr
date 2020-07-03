using FluentValidation;
using Sporterr.Cadastro.Domain;
using Sporterr.Cadastro.UnitTests.Fixtures;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
            Assert.Throws<ValidationException>(() => _fixtureWrapper.Empresa.CriarEmpresaInvalida());
        }

        [Fact(DisplayName = "Nova empresa válida")]
        [Trait("Domain", "Testes da Empresa")]
        public void Usuario_Validate_EmpresaDeveSerValida()
        {
            //Arrange & Act
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();

            //Assert
            Assert.NotNull(empresa);
            Assert.True(empresa.Ativo);
        }

        [Fact(DisplayName = "Adiciona nova solicitação na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AdicionarSolicitacao_DeveAdicionarUmaNovaSolicitacaoNaEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Act            
            empresa.AdicionarSolicitacao(solicitacao);

            //Assert
            Assert.Equal(1, empresa.Solicitacoes.Count);
            Assert.Same(solicitacao, empresa.Solicitacoes.SingleOrDefault());
        }



        [Fact(DisplayName = "Adiciona solicitação já existente na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AdicionarSolicitacao_DeveFalhaPoisJaExisteSolicitacaoIgualNaEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();
            empresa.AdicionarSolicitacao(solicitacao);

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.AdicionarSolicitacao(solicitacao));
        }


        [Fact(DisplayName = "Aprovar solicitação da empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AprovarSolicitacao_DeveAprovarSolicitacaoComSucesso()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();
            empresa.AdicionarSolicitacao(solicitacao);

            //Act 
            empresa.AprovarSolicitacao(solicitacao);

            //Assert
            Assert.Equal(Core.Enums.StatusSolicitacao.Aprovada, solicitacao.Status);
        }


        [Fact(DisplayName = "Aprovar solicitação inexistente na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_AprovarSolicitacao_DeveFalharPoisSolicitacaoNaoConstaNaEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.AprovarSolicitacao(solicitacao));
        }

        [Fact(DisplayName = "Recusar solicitação na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_RecusarSolicitacao_DeveRecusarSolicitacaoComSucesso()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();
            empresa.AdicionarSolicitacao(solicitacao);

            //Act 
            empresa.RecusarSolicitacao(solicitacao, "Solicitação recusar por falta de quadra para locação.");

            //Assert
            Assert.Equal(Core.Enums.StatusSolicitacao.Recusada, solicitacao.Status);
        }

        [Fact(DisplayName = "Recusar solicitação inexistente na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_RecusarSolicitacao_DeveFalharPoisSolicitacaoNaoConstaNaEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.RecusarSolicitacao(solicitacao, "Solicitação recusar por falta de quadra para locação."));
        }

        [Fact(DisplayName = "Recusar solicitação sem motivo na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_RecusarSolicitacao_DeveFalharPoisMotivoDoRecusoNaoFoiInformado()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();
            empresa.AdicionarSolicitacao(solicitacao);

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.RecusarSolicitacao(solicitacao, string.Empty));
        }

        [Fact(DisplayName = "cancelar solicitação na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_CancelarSolicitacao_DeveCancelarSolicitacaoComSucesso()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();
            empresa.AdicionarSolicitacao(solicitacao);

            //Act 
            empresa.CancelarSolicitacao(solicitacao, "Solicitação recusar por falta de quadra para locação.");

            //Assert
            Assert.Equal(Core.Enums.StatusSolicitacao.Cancelada, solicitacao.Status);
        }

        [Fact(DisplayName = "Cancelar solicitação inexistente na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_CancelarSolicitacao_DeveFalharPoisSolicitacaoNaoConstaNaEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.CancelarSolicitacao(solicitacao, "Solicitação recusar por falta de quadra para locação."));
        }

        [Fact(DisplayName = "Cancelar solicitação sem motivo na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_CancelarSolicitacao_DeveFalharPoisMotivoDoCancelamentoNaoFoiInformado()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();
            empresa.AdicionarSolicitacao(solicitacao);

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.CancelarSolicitacao(solicitacao, string.Empty));
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
            Assert.Equal(1, empresa.Quadras.Count);
            Assert.Same(quadra, empresa.Quadras.SingleOrDefault());
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
            Assert.Throws<DomainException>(() => empresa.AdicionarQuadra(quadra));
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
            Assert.False(quadra.Ativo);
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
            Assert.Throws<DomainException>(() => empresa.InativarQuadra(quadra));
        }

        [Fact(DisplayName = "Inativar quadra não pertencente a empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_InativarQuadra_DeveFalharPoisQuadraNaoPertenceEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.InativarQuadra(quadra));
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
            Assert.True(quadra.Ativo);
        }

        [Fact(DisplayName = "Reativar quadra já ativa da empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_ReativarQuadra_DeveFalharPoisQuadraJaEstaAtiva()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.ReativarQuadra(quadra));
        }

        [Fact(DisplayName = "Reativar quadra não pertencente a empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_ReativarQuadra_DeveFalharPoisQuadraNaoPertenceEmpresa()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();
            quadra.Empresa = empresa;
            quadra.Inativar();

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.ReativarQuadra(quadra));
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
            Assert.True(quadra.EmManutencao);
        }

        [Fact(DisplayName = "Coloca quadra que já esta em manutenção, em manutenção na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_ColocarQuadraEmManutencao_DeveFalharPoisQuadraJaEstaEmManutencao()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.ColocarQuadraEmManutencao(quadra));
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
            Assert.False(quadra.EmManutencao);
        }

        [Fact(DisplayName = "Retira quadra que não esta em manutenção da manutenção na empresa")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_RetirarQuadraDeManutencao_DeveFalharPoisQuadraNaoEstaEmManutencao()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Quadra quadra = _fixtureWrapper.Quadra.CriarQuadraValida();

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.RetirarQuadraDeManutencao(quadra));
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
            Assert.Equal(novoHorarioAbertura, empresa.HorarioAbertura);
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
            Assert.Equal(novoHorarioFechamento, empresa.HorarioFechamento);
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
            Assert.Equal(novoHorarioAbertura, empresa.HorarioAbertura);
            Assert.Equal(novoHorarioFechamento, empresa.HorarioFechamento);
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
            Assert.Equal((int)diasSemanaFuncionamentoAtual + DiasSemanaFuncionamento.Sabado, empresa.DiasFuncionamento);
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
            Assert.Equal((int)diasSemanaFuncionamentoAtual - DiasSemanaFuncionamento.Quarta, empresa.DiasFuncionamento);
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
            Assert.Equal(DiasSemanaFuncionamento.Todos, empresa.DiasFuncionamento);
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
            Assert.False(empresa.Ativo);
        }


        [Fact(DisplayName = "Inativa empresa com solicitações pendentes")]
        [Trait("Domain", "Testes da Empresa")]
        public void Empresa_Inativar_DeveFalharPoisHaSolicitacoesPendentes()
        {
            //Arrange
            Empresa empresa = _fixtureWrapper.Empresa.CriarEmpresaValida();
            Solicitacao solicitacao = _fixtureWrapper.Solicitacao.CriarSolicitacaoValida();
            empresa.AdicionarSolicitacao(solicitacao);

            //Act & Assert
            Assert.Throws<DomainException>(() => empresa.Inativar());
        }
    }
}
