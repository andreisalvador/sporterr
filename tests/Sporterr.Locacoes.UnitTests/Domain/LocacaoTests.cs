using FluentValidation;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Locacoes.Domain;
using Sporterr.Locacoes.Domain.Enums;
using Sporterr.Locacoes.UnitTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Locacoes.UnitTests.Domain
{
    [Collection(nameof(FixtureWrapper))]
    public class LocacaoTests
    {
        private readonly FixtureWrapper _fixtureWrapper;

        public LocacaoTests(FixtureWrapper fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
        }


        [Fact(DisplayName = "Nova locação válida")]
        [Trait("Domain", "Testes de Locação")]
        public void Locacao_Validate_DeveCriarLocacaoValida()
        {
            //Arrange & Act
            Locacao locacao = _fixtureWrapper.Locacao.CriarLocacaoValida();

            //Assert
            Assert.NotNull(locacao);

        }
        [Fact(DisplayName = "Calculo valor locação")]
        [Trait("Domain", "Testes de Locação")]
        public void Locacao_CalcularValorLocacao_DeveCriarLocacaoComValorCalculado()
        {
            //Arrange & Act
            Locacao locacao = _fixtureWrapper.Locacao.CriarLocacaoValida();

            //Assert
            Assert.Equal(600m, locacao.Valor);

        }

        [Fact(DisplayName = "Nova locação inválida")]
        [Trait("Domain", "Testes de Locação")]
        public void Locacao_Validate_DeveCriarLocacaoInvalida()
        {
            //Arrange & Act & Assert
            Assert.Throws<ValidationException>(() => _fixtureWrapper.Locacao.CriarLocacaoInvalida());
        }


        [Fact(DisplayName = "Cancelar locação")]
        [Trait("Domain", "Testes de Locação")]
        public void Locacao_Cancelar_DeveAplicarStatusDeCanceladoNaLocacao()
        {
            //Arrange
            Locacao locacao = _fixtureWrapper.Locacao.CriarLocacaoValida();           

            //Act
            locacao.Cancelar();

            //Assert
            Assert.Equal(StatusLocacao.Cancelada, locacao.Status);            
        }
    }
}
