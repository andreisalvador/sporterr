using MediatR;
using Sporterr.Cadastro.Application.Events;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Data;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.Core.Messages.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Application.Commands.Handlers
{
    public class EmpresaCommandHandler : BaseCommandHandler<Empresa>,
        IRequestHandler<AdicionarQuadraEmpresaUsuarioCommand, bool>,
        IRequestHandler<AbrirSolicitacaoLocacaoParaEmpresaCommand, bool>,
        IRequestHandler<AprovarSolicitacaoLocacaoCommand,bool>,
        IRequestHandler<RecusarSolicitacaoLocacaoCommand, bool>,        
        IRequestHandler<CancelarSolicitacaoLocacaoEmpresaCommand, bool>
    {

        private readonly IEmpresaRepository _empresaRepository;  
        public EmpresaCommandHandler(IEmpresaRepository empresaRepository, IMediatrHandler mediatr) : base(empresaRepository, mediatr)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<bool> Handle(AdicionarQuadraEmpresaUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Quadra novaQuadra = new Quadra(message.EmpresaId, message.TempoLocacao, message.ValorTempoLocado, message.TipoEsporteQuadra);
            empresa.AdicionarQuadra(novaQuadra);

            return await SaveAndPublish(new QuadraEmpresaUsuarioAdicionadaEvent(message.UsuarioId, novaQuadra.EmpresaId, novaQuadra.Id,
                                                            novaQuadra.TempoLocacao, novaQuadra.ValorTempoLocado, novaQuadra.TipoEsporteQuadra));
        }

        public async Task<bool> Handle(AbrirSolicitacaoLocacaoParaEmpresaCommand message, CancellationToken cancellationToken)
        {
            if(!message.IsValid()) return false;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Solicitacao novaSolicitacao = new Solicitacao(message.LocacaoId, empresa.Id, message.QuadraId);

            empresa.AdicionarSolicitacao(novaSolicitacao);

            _empresaRepository.AtualizarEmpresa(empresa);

            return await SaveAndPublish(new SolicitacaoAbertaEvent(novaSolicitacao.LocacaoId, novaSolicitacao.Id, novaSolicitacao.EmpresaId, novaSolicitacao.QuadraId, novaSolicitacao.Status));
        }

        public async Task<bool> Handle(AprovarSolicitacaoLocacaoCommand message, CancellationToken cancellationToken)
        {
            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Solicitacao solicitacaoParaAprovar = await _empresaRepository.ObterSolicitacaoPorId(message.SolicitacaoId);

            if (solicitacaoParaAprovar == null) return await NotifyAndReturn("Solicitação não encontrada.");

            empresa.AprovarSolicitacao(solicitacaoParaAprovar);
            _empresaRepository.AtualizarEmpresa(empresa);

            return await SaveAndPublish(new SolicitacaoLocacaoAprovadaEvent(solicitacaoParaAprovar.LocacaoId));
        }

        public async Task<bool> Handle(RecusarSolicitacaoLocacaoCommand message, CancellationToken cancellationToken)
        {
            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Solicitacao solicitacaoParaRecusar = await _empresaRepository.ObterSolicitacaoPorId(message.SolicitacaoId);

            if (solicitacaoParaRecusar == null) return await NotifyAndReturn("Solicitação não encontrada.");

            empresa.RecusarSolicitacao(solicitacaoParaRecusar, message.Motivo);
            _empresaRepository.AtualizarEmpresa(empresa);

            return await SaveAndPublish(new SolicitacaoLocacaoRecusadaEvent(solicitacaoParaRecusar.LocacaoId));
        }

        public async Task<bool> Handle(CancelarSolicitacaoLocacaoEmpresaCommand message, CancellationToken cancellationToken)
        {
            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Solicitacao solicitacaoParaCancelar = empresa.Solicitacoes.SingleOrDefault(s => s.LocacaoId.Equals(message.LocacaoId));

            if (solicitacaoParaCancelar == null) return await NotifyAndReturn("Solicitação não encontrada.");

            empresa.CancelarSolicitacao(solicitacaoParaCancelar, message.MotivoCancelamento);
            _empresaRepository.AtualizarEmpresa(empresa);

            return await SaveAndPublish(new SolicitacaoLocacaoCanceladaEvent(solicitacaoParaCancelar.Id, message.LocacaoId));
        }        
    }
}
