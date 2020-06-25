using FluentValidation;
using Sporterr.Core.Messages;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AdicionarUsuarioCommand : Command<AdicionarUsuarioCommand>
    {
        public string NomeUsuario { get; private set; }
        public string EmailUsuario { get; private set; }
        public string SenhaUsuario { get; set; }
        public AdicionarUsuarioCommand(string nomeUsuario, string emailUsuario, string senhaUsuario)
            : base(new AdicionarUsuarioValidation())
        {
            NomeUsuario = nomeUsuario;
            EmailUsuario = emailUsuario;
            SenhaUsuario = senhaUsuario;
        }

        private class AdicionarUsuarioValidation : AbstractValidator<AdicionarUsuarioCommand>
        {
            public AdicionarUsuarioValidation()
            {
                RuleFor(u => u.NomeUsuario)
                    .NotEmpty().WithMessage("O nome do usuário não pode ser vázio.");

                RuleFor(u => u.EmailUsuario)
                    .NotEmpty().WithMessage("O email do usuário não pode ser vázio.");

                RuleFor(u => u.SenhaUsuario)
                    .NotEmpty().WithMessage("A senha do usuário não pode ser vázia.");
            }
        }
    }
}
