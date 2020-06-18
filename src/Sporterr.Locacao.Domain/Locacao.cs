using FluentValidation;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using System;

namespace Sporterr.Locacoes.Domain
{
    public class Locacao : Entity<Locacao>, IAggregateRoot
    {
        public Guid UsuarioLocatarioId { get; set; }
        public Quadra Quadra { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataHoraInicioLocacao { get; private set; }
        public DateTime DataHoraFimLocacao { get; private set; }

        public Locacao(Guid usuarioLocatarioId, Quadra quadra, DateTime dataHoraLocacao, DateTime dataHoraFimLocacao)
        {
            UsuarioLocatarioId = usuarioLocatarioId;
            Quadra = quadra;
            Valor = CalcularValorLocacao(Quadra.ValorTempoQuadra, Quadra.TempoLocacaoQuadra);
            DataHoraInicioLocacao = dataHoraLocacao;
            DataHoraFimLocacao = dataHoraFimLocacao;
            Validar();
        }

        public void CalcularValorLocacao()
        {
            Valor = CalcularValorLocacao(Quadra.ValorTempoQuadra, Quadra.TempoLocacaoQuadra);
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
