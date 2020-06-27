using FluentValidation;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Locacoes.Domain.Enums;
using Sporterr.Locacoes.Domain.Validations;
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


        protected Locacao() { /*Ef core.*/}

        public Locacao(Guid usuarioLocatarioId, Guid empresaId, Quadra quadra, DateTime dataHoraInicioLocacao, DateTime dataHoraFimLocacao)
        {
            UsuarioLocatarioId = usuarioLocatarioId;
            EmpresaId = empresaId;
            Quadra = quadra;
            Valor = CalcularValorLocacao(Quadra.ValorPorTempoLocadoQuadra, Quadra.TempoLocacaoQuadra);
            DataHoraInicioLocacao = dataHoraInicioLocacao;
            DataHoraFimLocacao = dataHoraFimLocacao;
            Status = StatusLocacao.EmAberto;
            Validar();
        }

        public void CalcularValorLocacao()
        {
            Valor = CalcularValorLocacao(Quadra.ValorPorTempoLocadoQuadra, Quadra.TempoLocacaoQuadra);
        }

        public void AguardarAprovacao(Guid solicitacaoId)
        {
            if (Status == StatusLocacao.EmAberto) throw new DomainException("Não é possível aplicar o status de aguardando aprovação em uma locação que não estava 'em aberto'.");

            if (Status == StatusLocacao.Aprovada) throw new DomainException("Não é possível solicitar aprovação de locações já aprovadas.");

            Status = StatusLocacao.AguardandoAprovacao;
            SolicitacaoId = solicitacaoId;
        }
        public void AprovarLocacao()
        {
            if (Status != StatusLocacao.AguardandoAprovacao) throw new DomainException("Não é possível aprovar locações que não estavam aguardando aprovação.");

            ValidarStatus(StatusLocacao.AguardandoAprovacao, "aprovar");

            Status = StatusLocacao.Aprovada;
        }
        public void RecusarLocacao()
        {
            ValidarStatus(StatusLocacao.AguardandoAprovacao, "recusar");

            Status = StatusLocacao.Recusada;
        }       

        public void CancelarLocacao()
        {
            ValidarStatus(StatusLocacao.AguardandoCancelamento, "aprovar");

            Status = StatusLocacao.Cancelada;
        }
        public void AguardarCancelamento() 
        {
            if (Status == StatusLocacao.Cancelada) throw new DomainException("Não é possível solicitar cancelamento de locações já canceladas.");

            Status = StatusLocacao.AguardandoCancelamento;
        }
        // TODO: Realizar calculo
        private decimal CalcularValorLocacao(decimal valorTempoLocacaoQuadra, TimeSpan tempoLocacaoQuadra)
        {
            return 0m;
        }

        private void ValidarStatus(StatusLocacao status, string processo)
        {
            string tipoAguardo = status == StatusLocacao.AguardandoAprovacao ? "aprovação" : "cancelamento";

            if (Status != status) throw new DomainException($"Não é possível {processo} locações que não estavam aguardando {tipoAguardo}.");
        }

        protected override AbstractValidator<Locacao> ObterValidador() => new LocacaoValidation();
    }
}
