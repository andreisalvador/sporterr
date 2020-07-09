using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.DTO;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Locacoes.Domain.Enums;
using Sporterr.Locacoes.Domain.Validations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporterr.Locacoes.Domain
{
    public class Locacao : Entity<Locacao>, IAggregateRoot
    {
        public Guid UsuarioLocatarioId { get; set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public decimal Valor { get; private set; }
        public TimeSpan TempoLocado { get; private set; }
        public StatusLocacao Status { get; private set; }

        [NotMapped]
        public InformacoesTempoQuadra InformacoesTempoQuadra { get; private set; }
        public Guid SolicitacaoId { get; private set; }

        //Ef rel.
        public Solicitacao Solicitacao { get; set; }

        protected Locacao() { /*Ef core.*/}

        public Locacao(Guid solicitacaoId, Guid usuarioLocatarioId, Guid empresaId, Guid quadraId, TimeSpan tempoLocado, InformacoesTempoQuadra informacoesTempoQuadra)
        {
            UsuarioLocatarioId = usuarioLocatarioId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            SolicitacaoId = solicitacaoId;
            TempoLocado = tempoLocado;
            Valor = CalcularValorLocacao(informacoesTempoQuadra.ValorPorTempoLocadoQuadra, informacoesTempoQuadra.TempoLocacaoQuadra);
            Status = StatusLocacao.Aprovada;
            Validate();
        }

        public void CalcularValorLocacao()
        {
            Valor = CalcularValorLocacao(InformacoesTempoQuadra.ValorPorTempoLocadoQuadra, InformacoesTempoQuadra.TempoLocacaoQuadra);
        }
      
        public void Cancelar()
        {
            if (Status != StatusLocacao.Aprovada) throw new DomainException($"Não é possível cancelar locações que não estavam aprovadas.");

            Status = StatusLocacao.Cancelada;
        }
        
        // TODO: Realizar calculo
        private decimal CalcularValorLocacao(decimal valorTempoLocacaoQuadra, TimeSpan tempoLocacaoQuadra)
        {
            return (decimal)(TempoLocado / tempoLocacaoQuadra) * valorTempoLocacaoQuadra;
        }

        public override void Validate() => Validate(this, new LocacaoValidation());
    }
}
