using FluentAssertions;
using FluentValidation;
using Sporterr.Sorteio.Domain.UnitTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests
{
    [Collection(nameof(FixtureWrapper))]
    public class HabilidadeUsuarioTests
    {
        private readonly FixtureWrapper _fixtureWrapper;

        public HabilidadeUsuarioTests(FixtureWrapper fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
        }

        [Fact(DisplayName ="Cria habilidade de usuário válida")]
        [Trait("Domain", "Testes de habilidade de usuário")]
        public void HabilidadeUsuario_Validate_DeveCriarHabilidadeUsuarioValida()
        {
            //Arrange & Act
            HabilidadeUsuario habilidadeUsuario = _fixtureWrapper.HabilidadeUsuario.CriarHabilidadeUsuarioValido();

            //Assert
            habilidadeUsuario.Should().NotBeNull();
        }

        [Fact(DisplayName = "Cria habilidade de usuário inválida")]
        [Trait("Domain", "Testes de habilidade de usuário")]
        public void HabilidadeUsuario_Validate_DeveFalharPoisHabilidadeUsuarioEhInvalida()
        {
            //Arrange & Act & Assert
            _fixtureWrapper.HabilidadeUsuario.Invoking(x => x.CriarHabilidadeUsuarioInvalido()).Should().Throw<ValidationException>();
        }

        [Fact(DisplayName = "Adiciona uma coleção de avaliações na habilidade do usuário")]
        [Trait("Domain", "Testes de habilidade de usuário")]
        public void HabilidadeUsuario_AdicionarAvaliacoesHabilidade_DeveAdicionarUmaColecaoDeAvaliacoesNaHabilidadeDoUsuario()
        {
            //Arrange
            HabilidadeUsuario habilidadeUsuario = _fixtureWrapper.HabilidadeUsuario.CriarHabilidadeUsuarioValido();
            IEnumerable<AvaliacaoHabilidade> avaliacoes = new AvaliacaoHabilidade[]
            {
                _fixtureWrapper.AvaliacaoHabilidade.CriarAvaliacaoHabilidadeValida(),
                _fixtureWrapper.AvaliacaoHabilidade.CriarAvaliacaoHabilidadeValida(),
                _fixtureWrapper.AvaliacaoHabilidade.CriarAvaliacaoHabilidadeValida()
            };

            //Act
            habilidadeUsuario.AdicionarAvaliacoesHabilidade(avaliacoes);

            //Assert
            habilidadeUsuario.Avaliacoes.Should().HaveCount(3);
            habilidadeUsuario.Nota.Should().BeGreaterOrEqualTo(0);
            habilidadeUsuario.Nota.Should().Be(avaliacoes.Average(a => a.Nota));
            habilidadeUsuario.Id.Should().Be(avaliacoes.Select(s => s.HabilidadeUsuarioId).Distinct().Single());
        }

        [Fact(DisplayName = "Adiciona uma coleção de avaliações na habilidade do usuário")]
        [Trait("Domain", "Testes de habilidade de usuário")]
        public void HabilidadeUsuario_AdicionarAvaliacaoHabilidade_DeveAdicionarUmaAvaliacaoNaHabilidadeDoUsuario()
        {
            //Arrange
            HabilidadeUsuario habilidadeUsuario = _fixtureWrapper.HabilidadeUsuario.CriarHabilidadeUsuarioValido();
            AvaliacaoHabilidade avaliacao = _fixtureWrapper.AvaliacaoHabilidade.CriarAvaliacaoHabilidadeValida();

            //Act
            habilidadeUsuario.AdicionarAvaliacaoHabilidade(avaliacao);

            //Assert
            habilidadeUsuario.Avaliacoes.Should().HaveCount(1);            
            habilidadeUsuario.Nota.Should().BeGreaterOrEqualTo(0);
            habilidadeUsuario.Nota.Should().Be(avaliacao.Nota);
            habilidadeUsuario.Id.Should().Be(avaliacao.HabilidadeUsuarioId);
        }
    }
}
