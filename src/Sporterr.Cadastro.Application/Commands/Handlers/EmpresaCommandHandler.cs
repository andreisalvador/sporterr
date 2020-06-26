using MediatR;
using Sporterr.Cadastro.Application.Events;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Data;
using Sporterr.Core.DomainObjects.Exceptions;
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
        IRequestHandler<AdicionarQuadraEmpresaCommand, bool>,
        IRequestHandler<AbrirSolicitacaoLocacaoParaEmpresaCommand, bool>,
        IRequestHandler<AprovarSolicitacaoEmpresaCommand, bool>,
        IRequestHandler<RecusarSolicitacaoLocacaoCommand, bool>,
        IRequestHandler<CancelarSolicitacaoLocacaoEmpresaCommand, bool>,
        IRequestHandler<InativarQuadraEmpresaCommand, bool>
    {

        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaCommandHandler(IEmpresaRepository empresaRepository, IMediatrHandler mediatr) : base(empresaRepository, mediatr)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<bool> Handle(AdicionarQuadraEmpresaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Quadra novaQuadra = new Quadra(message.TipoEsporteQuadra, message.TempoLocacao, message.ValorPorTempoLocado);

            empresa.AdicionarQuadra(novaQuadra);

            _empresaRepository.AdicionarQuadra(novaQuadra);

            return await SaveAndPublish(new QuadraAdicionadaEmpresaEvent(message.UsuarioId, novaQuadra.EmpresaId, novaQuadra.Id,
                                                            novaQuadra.TempoLocacao, novaQuadra.ValorPorTempoLocado, novaQuadra.TipoEsporteQuadra));
        }

        public async Task<bool> Handle(AbrirSolicitacaoLocacaoParaEmpresaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Solicitacao novaSolicitacao = new Solicitacao(message.LocacaoId, message.QuadraId);

            empresa.AdicionarSolicitacao(novaSolicitacao);

            _empresaRepository.AdicionarSolicitacao(novaSolicitacao);

            return await SaveAndPublish(new SolicitacaoAbertaEvent(novaSolicitacao.LocacaoId, novaSolicitacao.Id, novaSolicitacao.EmpresaId, novaSolicitacao.QuadraId, novaSolicitacao.Status));
        }

        public async Task<bool> Handle(AprovarSolicitacaoEmpresaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Solicitacao solicitacaoParaAprovar = await _empresaRepository.ObterSolicitacaoPorId(message.SolicitacaoId);

            if (solicitacaoParaAprovar == null) return await NotifyAndReturn($"Solicitação não encontrada para empresa {empresa.RazaoSocial}.");

            empresa.AprovarSolicitacao(solicitacaoParaAprovar);

            _empresaRepository.AtualizarSolicitacao(solicitacaoParaAprovar);
            _empresaRepository.AtualizarEmpresa(empresa);

            return await SaveAndPublish(new SolicitacaoLocacaoAprovadaEvent(empresa.Id, solicitacaoParaAprovar.LocacaoId));
        }

        public async Task<bool> Handle(RecusarSolicitacaoLocacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Solicitacao solicitacaoParaRecusar = await _empresaRepository.ObterSolicitacaoPorId(message.SolicitacaoId);

            if (solicitacaoParaRecusar == null) return await NotifyAndReturn($"Solicitação não encontrada para empresa {empresa.RazaoSocial}.");

            empresa.RecusarSolicitacao(solicitacaoParaRecusar, message.Motivo);

            _empresaRepository.AtualizarEmpresa(empresa);

            return await SaveAndPublish(new SolicitacaoLocacaoRecusadaEvent(solicitacaoParaRecusar.LocacaoId));
        }

        public async Task<bool> Handle(CancelarSolicitacaoLocacaoEmpresaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Solicitacao solicitacaoParaCancelar = await _empresaRepository.ObterSolicitacaoPorId(message.SolicitacaoId);

            if (solicitacaoParaCancelar == null) return await NotifyAndReturn($"Solicitação não encontrada para empresa {empresa.RazaoSocial}.");

            empresa.CancelarSolicitacao(solicitacaoParaCancelar, message.MotivoCancelamento);

            _empresaRepository.AtualizarSolicitacao(solicitacaoParaCancelar);

            return await SaveAndPublish(new SolicitacaoLocacaoCanceladaEvent(solicitacaoParaCancelar.Id, message.LocacaoId));
        }

        public async Task<bool> Handle(InativarQuadraEmpresaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Quadra quadraParaInativar = await _empresaRepository.ObterQuadraPorId(message.QuadraId);

            if (quadraParaInativar == null) return await NotifyAndReturn($"Quadra não encontrada na empresa {empresa.RazaoSocial}.");

            try
            {
                empresa.InativarQuadra(quadraParaInativar);

                _empresaRepository.AtualizarQuadra(quadraParaInativar);

                _empresaRepository.AtualizarEmpresa(empresa);
            }
            catch (DomainException exception)
            {
                return await NotifyAndReturn(exception.Message);
            }

            return await SaveAndPublish(new QuadraInativadaEmpresaEvent(quadraParaInativar.Id, empresa.Id));
        }
    }
}
