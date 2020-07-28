using FluentValidation;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AdicionarGrupoCommand : Command<AdicionarGrupoCommand>
    {
        public Guid UsuarioCriadorId { get; private set; }
        public string NomeGrupo { get; private set; }
        public byte NumeroMaximoMembros { get; private set; }

        public AdicionarGrupoCommand(Guid usuarioCriadorId, string nomeGrupo, byte numeroMaximoMembros) : base(new AdicionarGrupoUsuarioValidation())
        {
            UsuarioCriadorId = usuarioCriadorId;
            NomeGrupo = nomeGrupo;
            NumeroMaximoMembros = numeroMaximoMembros;
        }
        
        private class AdicionarGrupoUsuarioValidation : AbstractValidator<AdicionarGrupoCommand>
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
