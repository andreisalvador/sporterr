using FluentAssertions;
using FluentValidation;
using Sporterr.Sorteio.Domain.UnitTests.Fixtures;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sporterr.Sorteio.Domain.UnitTests
{
    [Collection(nameof(FixtureWrapper))]
    public class PerfilHabilidadesTests
    {
        private readonly FixtureWrapper _fixtureWrapper;

        public PerfilHabilidadesTests(FixtureWrapper fixtureWrapper)
        {
            _fixtureWrapper = fixtureWrapper;
        }

        [Fact(DisplayName = "Cria perfil de habilidades válido")]
        [Trait("Domain", "Testes de perfil de habilidades")]
        public void PerfilHabilidades_Validate_DeveCriarPerfilHabilidadesValido()
        {
            //Arrange & Act
            PerfilHabilidades novoPerfil = _fixtureWrapper.PerfilHabilidades.CriarPerfilHabilidadeValido();

            //Assert
            novoPerfil.Should().NotBeNull();
            novoPerfil.Ativo.Should().BeTrue();
        }

        [Fact(DisplayName = "Cria perfil de habilidades inválido")]
        [Trait("Domain", "Testes de perfil de habilidades")]
        public void PerfilHabilidades_Validate_DeveFalharPoisPerfilEhInvalido()
        {
            //Arrange & Act & Assert
            _fixtureWrapper.PerfilHabilidades.Invoking(x => x.CriarPerfilHabilidadeInvalido()).Should().Throw<ValidationException>();
        }

        [Fact(DisplayName ="Adiciona uma coleção de habilidades ao perfil")]
        [Trait("Domain", "Testes de perfil de habilidades")]
        public void PerfilHabilidades_AdicionarHabilidadesUsuario_DeveAdicionarUmaColecaoDeHabilidadesDeUsuarioAoPerfil()
        {
            //Arrange
            PerfilHabilidades perfil = _fixtureWrapper.PerfilHabilidades.CriarPerfilHabilidadeValido();

            IEnumerable<HabilidadeUsuario> habilidades = new HabilidadeUsuario[]
            {
                _fixtureWrapper.HabilidadeUsuario.CriarHabilidadeUsuarioValido(),
                _fixtureWrapper.HabilidadeUsuario.CriarHabilidadeUsuarioValido()
            };

            //Act
            perfil.AdicionarHabilidadesUsuario(habilidades);

            //Assert
            perfil.HabilidadesUsuario.Should().HaveCount(2);            
        }

        [Fact(DisplayName = "Adiciona uma habilidade ao perfil")]
        [Trait("Domain", "Testes de perfil de habilidades")]
        public void PerfilHabilidades_AdicionarHabilidadeUsuario_DeveAdicionarUmaHabilidadeDeUsuarioAoPerfil()
        {
            //Arrange
            PerfilHabilidades perfil = _fixtureWrapper.PerfilHabilidades.CriarPerfilHabilidadeValido();

            HabilidadeUsuario habilidade = _fixtureWrapper.HabilidadeUsuario.CriarHabilidadeUsuarioValido();
            //Act
            perfil.AdicionarHabilidadeUsuario(habilidade);

            //Assert
            perfil.HabilidadesUsuario.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Retorna habilidades procurando por esporte")]
        [Trait("Domain", "Testes de perfil de habilidades")]
        public void PerfilHabilidades_ObterHabilidadesPorEsporte_DeveRetornarHabilidadesProcurandoPorEsporte()
        {
            //Arrange
            PerfilHabilidades perfil = _fixtureWrapper.PerfilHabilidades.CriarPerfilHabilidadeValido();
            Esporte esporte = _fixtureWrapper.Esporte.CriarEsporteValido();

            IEnumerable<HabilidadeUsuario> habilidades = new HabilidadeUsuario[]
            {
                _fixtureWrapper.HabilidadeUsuario.CriarHabilidadeUsuarioValido(),
                _fixtureWrapper.HabilidadeUsuario.CriarHabilidadeUsuarioValido()
            };

            foreach (HabilidadeUsuario habilidade in habilidades)
                habilidade.Esporte = esporte;

            perfil.AdicionarHabilidadesUsuario(habilidades);

            //Act
            IEnumerable<HabilidadeUsuario> habilidadesEncontradas = perfil.ObterHabilidadesPorEsporte(esporte.TipoEsporte);

            //Assert
            perfil.HabilidadesUsuario.Should().HaveCount(2);
        }

        [Fact(DisplayName ="Vincula esporte na habilidade", Skip = "Preciso planejar como vincular")]
        [Trait("Domain", "Testes de perfil de habilidades")]
        public void PerfilHabilidades_VincularEsporte_DeveVincularEsporteNaHabilidade()
        {

        }
    }
}
