using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AdicionarGrupoUsuarioCommand : Command<AdicionarGrupoUsuarioCommand>
    {
        public Guid UsuarioCriadorId { get; private set; }
        public string NomeGrupo { get; private set; }
        public sbyte NumeroMaximoMembros { get; private set; }

        public AdicionarGrupoUsuarioCommand(Guid usuarioCriadorId, string nomeGrupo, sbyte numeroMaximoMembros)
        {
            UsuarioCriadorId = usuarioCriadorId;
            NomeGrupo = nomeGrupo;
            NumeroMaximoMembros = numeroMaximoMembros;
        }

        protected override AbstractValidator<AdicionarGrupoUsuarioCommand> GetValidator() => new AdicionarGrupoUsuarioValidation();
        
        private class AdicionarGrupoUsuarioValidation : AbstractValidator<AdicionarGrupoUsuarioCommand>
        {
            public AdicionarGrupoUsuarioValidation()
            {
                RuleFor(g => g.UsuarioCriadorId)
                    .NotEqual(Guid.Empty).WithMessage("O 'id' do usuário não pode ser vazio.");

                RuleFor(g => g.NomeGrupo)
                    .NotEmpty().WithMessage("O nome do grupo não pode ser vazio.");
            }
        }
    }
}
