using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class InativarEmpresaUsuarioCommand : Command<InativarEmpresaUsuarioCommand>
    {
        public Guid UsuarioProprietarioEmpresaId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public InativarEmpresaUsuarioCommand(Guid usuarioProprietarioEmpresaId, Guid empresaId)
        {
            UsuarioProprietarioEmpresaId = usuarioProprietarioEmpresaId;
            EmpresaId = empresaId;
        }

        protected override AbstractValidator<InativarEmpresaUsuarioCommand> GetValidator()
        {
            throw new NotImplementedException();
        }
    }
}
