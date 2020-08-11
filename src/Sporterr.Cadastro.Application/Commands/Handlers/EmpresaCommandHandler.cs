using FluentValidation.Results;
using MediatR;
using Sporterr.Cadastro.Application.Events;
using Sporterr.Cadastro.Domain;
using Sporterr.Cadastro.Domain.Data.Interfaces;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.DomainObjects.DTO;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.Messages.CommonMessages.IntegrationEvents.Solicitacoes;
using Sporterr.Core.Messages.Handler;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Application.Commands.Handlers
{
    public class EmpresaCommandHandler : CommandHandler<Empresa>,
        IRequestHandler<AdicionarQuadraCommand, ValidationResult>,
        IRequestHandler<AprovarSolicitacaoLocacaoCommand, ValidationResult>,
        IRequestHandler<RecusarSolicitacaoLocacaoCommand, ValidationResult>,
        IRequestHandler<CancelarSolicitacaoLocacaoEmpresaCommand, ValidationResult>,
        IRequestHandler<InativarQuadraCommand, ValidationResult>
    {

        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaCommandHandler(IEmpresaRepository empresaRepository, IMediatrHandler mediatr) : base(empresaRepository, mediatr)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<ValidationResult> Handle(AdicionarQuadraCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa is null) return await NotifyAndReturn("Empresa não encontrada.");

            Quadra novaQuadra = new Quadra(message.TipoEsporteQuadra, message.TempoLocacao, message.ValorPorTempoLocado);

            empresa.AdicionarQuadra(novaQuadra);

            _empresaRepository.AdicionarQuadra(novaQuadra);

            return await SaveAndPublish(new QuadraAdicionadaEmpresaEvent(novaQuadra.Id, message.UsuarioId, novaQuadra.EmpresaId, novaQuadra.Id,
                                                            novaQuadra.TempoLocacao, novaQuadra.ValorPorTempoLocado, novaQuadra.TipoEsporteQuadra));
        }

        public async Task<ValidationResult> Handle(AprovarSolicitacaoLocacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            Empresa empresa = await _empresaRepository.ObterEmpresaComQuadrasPorId(message.EmpresaId);

            if (empresa is null) return await NotifyAndReturn("Empresa não encontrada.");

            Quadra quadraQueSeraLocada = empresa.Quadras.SingleOrDefault(q => q.Id.Equals(message.QuadraId));

            if (quadraQueSeraLocada is null) return await NotifyAndReturn($"Quadra não encontrada na empresa '{empresa.RazaoSocial}'.");

            return await PublishEvents(new SolicitacaoLocacaoAprovadaEvent(message.SolicitacaoId, empresa.Id, message.QuadraId,
                new InformacoesTempoQuadra(quadraQueSeraLocada.ValorPorTempoLocado, quadraQueSeraLocada.TempoLocacao)));
        }

        public async Task<ValidationResult> Handle(RecusarSolicitacaoLocacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            return await PublishEvents(new SolicitacaoLocacaoRecusadaEvent(message.SolicitacaoId, message.Motivo));
        }

        public async Task<ValidationResult> Handle(CancelarSolicitacaoLocacaoEmpresaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            return await PublishEvents(new SolicitacaoLocacaoCanceladaEvent(message.SolicitacaoId, message.MotivoCancelamento));
        }

        public async Task<ValidationResult> Handle(InativarQuadraCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(message.EmpresaId);

            if (empresa is null) return await NotifyAndReturn("Empresa não encontrada.");

            Quadra quadraParaInativar = await _empresaRepository.ObterQuadraPorId(message.QuadraId);

            if (quadraParaInativar is null) return await NotifyAndReturn($"Quadra não encontrada na empresa {empresa.RazaoSocial}.");

            try
            {
                empresa.InativarQuadra(quadraParaInativar);

                _empresaRepository.AtualizarQuadra(quadraParaInativar);

                _empresaRepository.AtualizarEmpresa(empresa);

                return await SaveAndPublish(new QuadraInativadaEmpresaEvent(quadraParaInativar.Id, empresa.Id));
            }
            catch (DomainException exception)
            {
                return await NotifyAndReturn(exception.Message);
            }
        }
    }
}
