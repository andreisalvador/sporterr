using FluentValidation;
using Sporterr.Cadastro.Domain.Enumns;
using Sporterr.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Domain
{
    public class Solicitacao : Entity<Solicitacao>
    {
        public Guid LocacaoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid QuadraId { get; private set; }
        public StatusSolicitacao Status { get; private set; }
        //Ef rel.
        public Empresa Empresa { get; set; }
        public Solicitacao(Guid locacaoId, Guid empresaId, Guid quadraId)
        {
            LocacaoId = locacaoId;
            EmpresaId = empresaId;
            QuadraId = quadraId;
        }

        public void Aprovar() => Status = StatusSolicitacao.Aprovada;
        public void Recusar() => Status = StatusSolicitacao.Recusada;

        protected override AbstractValidator<Solicitacao> ObterValidador()
        {
            throw new NotImplementedException();
        }
    }
}
