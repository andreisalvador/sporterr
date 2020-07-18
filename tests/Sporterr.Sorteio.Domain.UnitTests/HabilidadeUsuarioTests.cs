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
            Assert.NotNull(habilidadeUsuario);
        }

        [Fact(DisplayName = "Cria habilidade de usuário inválida")]
        [Trait("Domain", "Testes de habilidade de usuário")]
        public void HabilidadeUsuario_Validate_DeveFalharPoisHabilidadeUsuarioEhInvalida()
        {
            //Arrange & Act & Assert
            Assert.Throws<ValidationException>(() => _fixtureWrapper.HabilidadeUsuario.CriarHabilidadeUsuarioInvalido());
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
            Assert.Equal(3, habilidadeUsuario.Avaliacoes.Count);
            Assert.True(habilidadeUsuario.Nota >= 0);
            Assert.Equal(habilidadeUsuario.Nota, avaliacoes.Average(a => a.Nota));
            Assert.Equal(habilidadeUsuario.Id, avaliacoes.Select(s => s.HabilidadeUsuarioId).Distinct().Single());
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
            Assert.Equal(1, habilidadeUsuario.Avaliacoes.Count);
            Assert.True(habilidadeUsuario.Nota >= 0);
            Assert.Equal(habilidadeUsuario.Nota, avaliacao.Nota);
            Assert.Equal(habilidadeUsuario.Id, avaliacao.HabilidadeUsuarioId);
        }
    }
}
