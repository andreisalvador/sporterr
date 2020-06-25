using FluentValidation;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Locacoes.Domain.Enums;
using System;

namespace Sporterr.Locacoes.Domain
{
    public class Locacao : Entity<Locacao>, IAggregateRoot
    {
        public Guid UsuarioLocatarioId { get; set; }
        public Guid EmpresaId { get; private set; }
        public Quadra Quadra { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataHoraInicioLocacao { get; private set; }
        public DateTime DataHoraFimLocacao { get; private set; }
        public StatusLocacao Status { get; private set; }
        public Guid? SolicitacaoId { get; private set; }


        public Locacao(Guid usuarioLocatarioId, Guid empresaId, Quadra quadra, DateTime dataHoraInicioLocacao, DateTime dataHoraFimLocacao)
        {
            UsuarioLocatarioId = usuarioLocatarioId;
            EmpresaId = empresaId;
            Quadra = quadra;
            Valor = CalcularValorLocacao(Quadra.ValorTempoQuadra, Quadra.TempoLocacaoQuadra);
            DataHoraInicioLocacao = dataHoraInicioLocacao;
            DataHoraFimLocacao = dataHoraFimLocacao;
            Status = StatusLocacao.EmAberto;
            Validar();
        }

        public void CalcularValorLocacao()
        {
            Valor = CalcularValorLocacao(Quadra.ValorTempoQuadra, Quadra.TempoLocacaoQuadra);
        }

        public void AguardarAprovacao(Guid solicitacaoId)
        {
            Status = StatusLocacao.AguardandoAprovacao;
            SolicitacaoId = solicitacaoId;
        }
        public void AprovarLocacao() => Status = StatusLocacao.Aprovada;
        public void RecusarLocacao() => Status = StatusLocacao.Recusada;
        public void CancelarLocacao() => Status = StatusLocacao.Cancelada;
        public void AguardarCancelamento() => Status = StatusLocacao.AguardandoCancelamento;
        // TODO: Realizar calculo
        private decimal CalcularValorLocacao(decimal valorTempoLocacaoQuadra, TimeSpan tempoLocacaoQuadra)
        {
            return 0m;
        }



        protected override AbstractValidator<Locacao> ObterValidador()
        {
            throw new NotImplementedException();
        }
    }
}
