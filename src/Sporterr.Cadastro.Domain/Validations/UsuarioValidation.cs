using FluentValidation;
using Sporterr.Cadastro.Domain.Resources;

namespace Sporterr.Cadastro.Domain.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage(MensagensValidacaoCadastro.NomeUsuarioVazio)
                .MaximumLength(50).WithMessage(string.Format(MensagensValidacaoCadastro.QuantidadeMaximaCaracteresNome, 50))
                .MinimumLength(5).WithMessage(string.Format(MensagensValidacaoCadastro.QuantidadeMinimaCaracteresNome, 20));

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(MensagensValidacaoCadastro.EmailInvadio)
                .EmailAddress().WithMessage(MensagensValidacaoCadastro.EmailInvadio);

            RuleFor(u => u.Senha)
             .NotEmpty()
             .MinimumLength(8).WithMessage(string.Format(MensagensValidacaoCadastro.QuantidadeMinimaCaracteresSenha, 8))
             .MaximumLength(20).WithMessage(string.Format(MensagensValidacaoCadastro.QuantidadeMaximaCaracteresSenha, 20));
        }
    }
}
