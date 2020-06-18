using FluentValidation;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using System;

namespace Sporterr.Locacao.Domain
{
    public class Locacao : Entity<Locacao>, IAggregateRoot
    {
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public decimal ValorTempoQuadra  { get; private set; }
        public TimeSpan TempoLocacaoQuadra { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataHoraInicioLocacao { get; private set; }
        public DateTime DataHoraFimLocacao { get; private set; }

        public Locacao(Guid empresaId, Guid quadraId, decimal valorTempoQuadra, TimeSpan tempoLocacaoQuadra, DateTime dataHoraLocacao, DateTime dataHoraFimLocacao)
        {
            EmpresaId = empresaId;
            QuadraId = quadraId;
            ValorTempoQuadra = valorTempoQuadra;
            TempoLocacaoQuadra = tempoLocacaoQuadra;
            Valor = CalcularValorLocacao(ValorTempoQuadra, TempoLocacaoQuadra);
            DataHoraInicioLocacao = dataHoraLocacao;
            DataHoraFimLocacao = dataHoraFimLocacao;
            Validar();
        }

        public void CalcularValorLocacao()
        {
            Valor = CalcularValorLocacao(ValorTempoQuadra, TempoLocacaoQuadra);
        }
       
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
