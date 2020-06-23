using FluentValidation;
using Sporterr.Core.DomainObjects;
using Sporterr.Core.Enums;
using System;

namespace Sporterr.Cadastro.Domain
{
    public class Solicitacao : Entity<Solicitacao>
    {
        public Guid LocacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public StatusSolicitacao Status { get; private set; }
        public string? Motivo { get; private set; }
        //Ef rel.
        public Empresa Empresa { get; set; }
        public Solicitacao(Guid locacaoId, Guid empresaId, Guid quadraId)
        {
            LocacaoId = locacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
            Status = StatusSolicitacao.AguardandoAprovacao;
        }

        public void Aprovar() => Status = StatusSolicitacao.Aprovada;
        public void Recusar(string motivo)
        {
            Status = StatusSolicitacao.Recusada;
            AplicarMotivo(motivo);
        }

        private void AplicarMotivo(string motivo)
        {
            if (Status != StatusSolicitacao.AguardandoAprovacao)
                Motivo = motivo;
        }

        protected override AbstractValidator<Solicitacao> ObterValidador()
        {
            throw new NotImplementedException();
        }
    }
}
