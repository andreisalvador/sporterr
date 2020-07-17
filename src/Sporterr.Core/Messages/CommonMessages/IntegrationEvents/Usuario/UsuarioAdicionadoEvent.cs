using Sporterr.Core.Messages;
using System;

namespace Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Usuario
{
    public class UsuarioAdicionadoEvent : Event
    {
        public Guid UsuarioId { get; private set; }
        public string NomeUsuario { get; private set; }
        public string EmailUsuario { get; private set; }
        public string SenhaUsuario { get; private set; }

        public UsuarioAdicionadoEvent(Guid usuarioId, string nomeUsuario, string emailUsuario, string senhaUsuario)
        {
            AggregateId = usuarioId;
            UsuarioId = usuarioId;
            NomeUsuario = nomeUsuario;
            EmailUsuario = emailUsuario;
            SenhaUsuario = senhaUsuario;
        }
    }
}
