using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Events
{
    public class EmpresaInativadaEvent : Event
    {
        public Guid EmpresaId { get; private set; }
        public Guid UsuarioProprietarioEmpresaId { get; private set; }
        public EmpresaInativadaEvent(Guid empresaId, Guid usuarioProprietarioEmpresaId)
        {
            AggregateId = usuarioProprietarioEmpresaId;
            EmpresaId = empresaId;
            UsuarioProprietarioEmpresaId = usuarioProprietarioEmpresaId;
        }
    }
}
