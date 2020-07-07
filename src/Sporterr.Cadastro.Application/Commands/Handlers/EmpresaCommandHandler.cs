using MediatR;
using Sporterr.Cadastro.Application.Events;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Data;
using Sporterr.Core.DomainObjects.DTO;
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
        IRequestHandler<AprovarSolicitacaoLocacaoCommand, bool>,
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

            return await SaveAndPublish(new QuadraAdicionadaEmpresaEvent(novaQuadra.Id, message.UsuarioId, novaQuadra.EmpresaId, novaQuadra.Id,
                                                            novaQuadra.TempoLocacao, novaQuadra.ValorPorTempoLocado, novaQuadra.TipoEsporteQuadra));
        }

        public async Task<bool> Handle(AprovarSolicitacaoLocacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Empresa empresa = await _empresaRepository.ObterEmpresaComQuadrasPorId(message.EmpresaId);

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Quadra quadraQueSeraLocada = empresa.Quadras.SingleOrDefault(q => q.Id.Equals(message.QuadraId));

            if (quadraQueSeraLocada == null) return await NotifyAndReturn($"Quadra não encontrada na empresa '{empresa.RazaoSocial}'.");

            InformacoesTempoQuadra informacoesTempoQuadra = new InformacoesTempoQuadra
            {
                TempoLocacaoQuadra = quadraQueSeraLocada.TempoLocacao,
                ValorPorTempoLocadoQuadra = quadraQueSeraLocada.ValorPorTempoLocado
            };

            return await PublishEvents(new SolicitacaoLocacaoAprovadaEvent(message.SolicitacaoId, empresa.Id, message.QuadraId, informacoesTempoQuadra));
        }

        public async Task<bool> Handle(RecusarSolicitacaoLocacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            return await SaveAndPublish(new SolicitacaoLocacaoRecusadaEvent(message.SolicitacaoId, message.Motivo));
        }

        public async Task<bool> Handle(CancelarSolicitacaoLocacaoEmpresaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            return await PublishEvents(new SolicitacaoLocacaoCanceladaEvent(message.SolicitacaoId, message.MotivoCancelamento));
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
