using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Events
{
    public class GrupoUsuarioAdicionadoEvent : Event
    {
        public Guid UsuarioCriadorId { get; private set; }
        public Guid GrupoId { get; private set; }
        public string NomeGrupo { get; private set; }
        public sbyte NumeroMaximoMembros { get; private set; }

        public GrupoUsuarioAdicionadoEvent(Guid usuarioCriadorId, Guid grupoId, string nomeGrupo, sbyte numeroMaximoMembros)
        {
            AggregateId = usuarioCriadorId;
            UsuarioCriadorId = usuarioCriadorId;
            GrupoId = grupoId;
            NomeGrupo = nomeGrupo;
            NumeroMaximoMembros = numeroMaximoMembros;
        }
    }
}