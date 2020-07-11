using FluentValidation;
using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Commands
{
    public class AdicionarQuadraCommand : Command<AdicionarQuadraCommand>
    {
        public Guid UsuarioId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public decimal ValorPorTempoLocado { get; private set; }
        public TimeSpan TempoLocacao { get; private set; }
        public Esportes TipoEsporteQuadra { get; private set; }

        public AdicionarQuadraCommand(Guid usuarioId, Guid empresaId, decimal valorTempoLocado, TimeSpan tempoLocacao, Esportes tipoEsporteQuadra)
            : base(new AdicionarQuadraEmpresaUsuarioValidation())
        {
            UsuarioId = usuarioId;
            EmpresaId = empresaId;
            ValorPorTempoLocado = valorTempoLocado;
            TempoLocacao = tempoLocacao;
            TipoEsporteQuadra = tipoEsporteQuadra;
        }

        private class AdicionarQuadraEmpresaUsuarioValidation : AbstractValidator<AdicionarQuadraCommand>
        {
            public AdicionarQuadraEmpresaUsuarioValidation()
            {
                RuleFor(q => q.UsuarioId)
                    .NotEqual(Guid.Empty).WithMessage("O 'id' do usuário não pode ser vazio.");

                RuleFor(q => q.EmpresaId)
                    .NotEqual(Guid.Empty).WithMessage("O 'id' da empresa não pode ser vazio.");

                RuleFor(q => q.TempoLocacao)
                    .NotEqual(TimeSpan.MinValue).WithMessage("O tempo de locação da quadra não pode ser vazio.");

                RuleFor(q => q.ValorPorTempoLocado)
                    .GreaterThan(0).WithMessage("O valor do tempo de locação precisa ser maior que zero.");

                RuleFor(q => q.TipoEsporteQuadra)
                    .IsInEnum().WithMessage("O tipo de esporte informado não é válido.");
            }
        }
    }
}
