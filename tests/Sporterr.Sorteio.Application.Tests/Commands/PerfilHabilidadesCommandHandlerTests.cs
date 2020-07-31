using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sporterr.Sorteio.Application.Tests.Commands
{
    public class PerfilHabilidadesCommandHandlerTests
    {
        [Fact(DisplayName = "Cadastra novo perfil de habilidades")]
        [Trait("Application", "Testes dos comandos do perfil de habilidades")]
        public void PerfilHabilidadesCommandHandler_HandleAdicionarPerfilHabilidadesCommand_DeveCadastrarNovoPerfil()
        {
            AutoMocker autoMocker = new AutoMocker();
            
        }
    }
}
