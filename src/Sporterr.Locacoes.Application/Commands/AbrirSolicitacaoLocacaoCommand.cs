using FluentValidation;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Locacoes.Application.Commands
{
    public class AbrirSolicitacaoLocacaoCommand : Command<AbrirSolicitacaoLocacaoCommand>
    {
        public Guid UsuarioLocatarioId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public DateTime DataHoraInicioLocacao { get; private set; }
        public DateTime DataHoraFimLocacao { get; private set; }


        public AbrirSolicitacaoLocacaoCommand(Guid usuarioLocatarioId, Guid empresaId, Guid quadraId, DateTime dataHoraInicioLocacao, DateTime dataHoraFimLocacao)
            : base(new AbrirSolicitacaoLocacaoValidation())
        {
            UsuarioLocatarioId = usuarioLocatarioId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            DataHoraInicioLocacao = dataHoraInicioLocacao;
            DataHoraFimLocacao = dataHoraFimLocacao;
        }

        private class AbrirSolicitacaoLocacaoValidation : AbstractValidator<AbrirSolicitacaoLocacaoCommand>
        {
            public AbrirSolicitacaoLocacaoValidation()
            {
                RuleFor(s => s.UsuarioLocatarioId)
                    .NotEqual(Guid.Empty).WithMessage("O usuário locatário precisa ser informado.");

                RuleFor(s => s.EmpresaId)
                .NotEqual(Guid.Empty).WithMessage("A empresa precisa ser informada.");

                RuleFor(s => s.QuadraId)
                    .NotEqual(Guid.Empty).WithMessage("O quadra precisa ser informada.");

                RuleFor(s => s.DataHoraInicioLocacao)
                    .NotEqual(DateTime.MinValue).WithMessage("A data e hora de início da locação precisa ser informada.");

                RuleFor(s => s.DataHoraFimLocacao)
                    .NotEqual(DateTime.MinValue).WithMessage("A data e hora do fim da locação precisa ser informada.");
            }
        }
    }
}
