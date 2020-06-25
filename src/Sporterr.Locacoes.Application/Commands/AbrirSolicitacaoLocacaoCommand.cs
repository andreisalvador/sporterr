using FluentValidation;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Locacoes.Application.Commands
{
    public class AbrirSolicitacaoLocacaoCommand : Command<AbrirSolicitacaoLocacaoCommand>
    {
        public Guid UsuarioLocatarioId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public decimal ValorTempoQuadra { get; private set; }
        public TimeSpan TempoLocacaoQuadra { get; private set; }
        public DateTime DataHoraInicioLocacao { get; private set; }
        public DateTime DataHoraFimLocacao { get; private set; }


        public AbrirSolicitacaoLocacaoCommand(Guid usuarioLocatarioId, Guid empresaId, Guid quadraId, decimal valorTempoQuadra, 
                                              TimeSpan tempoLocacaoQuadra, DateTime dataHoraInicioLocacao, DateTime dataHoraFimLocacao)
            : base(new AbrirSolicitacaoLocacaoValidation())
        {
            UsuarioLocatarioId = usuarioLocatarioId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            ValorTempoQuadra = valorTempoQuadra;
            TempoLocacaoQuadra = tempoLocacaoQuadra;
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

                RuleFor(s => s.ValorTempoQuadra)
                    .GreaterThan(0).WithMessage("O valor do tempo de locação da quadra precisa ser maior que zero.");

                RuleFor(s => s.TempoLocacaoQuadra)
                    .NotEqual(TimeSpan.MinValue).WithMessage("O tempo de locação da quadra precisa ser informado.");

                RuleFor(s => s.DataHoraInicioLocacao)
                    .NotEqual(DateTime.MinValue).WithMessage("A data e hora de início da locação precisa ser informada.");

                RuleFor(s => s.DataHoraFimLocacao)
                    .NotEqual(DateTime.MinValue).WithMessage("A data e hora do fim da locação precisa ser informada.");
            }
        }
    }
}
