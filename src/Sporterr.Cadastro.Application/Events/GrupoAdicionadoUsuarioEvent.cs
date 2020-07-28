using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Events
{
    public class GrupoAdicionadoUsuarioEvent : Event
    {
        public Guid UsuarioCriadorId { get; private set; }
        public Guid GrupoId { get; private set; }
        public string NomeGrupo { get; private set; }
        public byte NumeroMaximoMembros { get; private set; }

        public GrupoAdicionadoUsuarioEvent(Guid usuarioCriadorId, Guid grupoId, string nomeGrupo, byte numeroMaximoMembros)
        {
            AggregateId = grupoId;
            UsuarioCriadorId = usuarioCriadorId;
            GrupoId = grupoId;
            NomeGrupo = nomeGrupo;
            NumeroMaximoMembros = numeroMaximoMembros;
        }
    }
}