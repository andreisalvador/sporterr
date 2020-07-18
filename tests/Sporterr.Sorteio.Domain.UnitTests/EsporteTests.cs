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
    public class EsporteTests
    {
        private readonly FixtureWrapper _fixtureWrapper;
        public EsporteTests(FixtureWrapper fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
        }

        [Fact(DisplayName ="Cria esporte válido")]
        [Trait("Domain", "Testes de Esporte")]
        public void Esporte_Validate_DeveCriarEsporteValido()
        {
            //Arrange & Act
            Esporte esporte = _fixtureWrapper.Esporte.CriarEsporteValido();

            //Assert
            Assert.NotNull(esporte);
        }

        [Fact(DisplayName = "Cria esporte inválido")]
        [Trait("Domain", "Testes de Esporte")]
        public void Esporte_Validate_DeveCriarEsporteInvalido()
        {
            //Arrange & Act & Assert
            Assert.Throws<ValidationException>(() => _fixtureWrapper.Esporte.CriarEsporteInvalido());
        }


        [Fact(DisplayName = "Adiciona habilidade ao esporte")]
        [Trait("Domain", "Testes de Esporte")]
        public void Esporte_AdicionarHabilidade_DeveAdicionarNovaHabilidadeAoEsporte()
        {
            //Arrange 
            Esporte esporte = _fixtureWrapper.Esporte.CriarEsporteValido();
            Habilidade habilidade = _fixtureWrapper.Habilidade.CriarHabilidadeValida();

            //Act 
            esporte.AdicionarHabilidade(habilidade);

            //Assert
            Assert.Equal(1, esporte.Habilidades.Count);
            Assert.Equal(esporte.Id, esporte.Habilidades.Single().EsporteId);
        }

        [Fact(DisplayName = "Remove habilidade do esporte")]
        [Trait("Domain", "Testes de Esporte")]
        public void Esporte_RemoverHablidade_DeveRemoverHablidadeDoEsporte()
        {
            //Arrange 
            Esporte esporte = _fixtureWrapper.Esporte.CriarEsporteValido();
            Habilidade habilidade = _fixtureWrapper.Habilidade.CriarHabilidadeValida();
            esporte.AdicionarHabilidade(habilidade);

            //Act 
            esporte.RemoverHablidade(habilidade);

            //Assert
            Assert.Equal(0, esporte.Habilidades.Count);
        }
    }
}
