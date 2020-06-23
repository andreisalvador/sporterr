using MediatR;
using Sporterr.Cadastro.Application.Events;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Messages.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Application.Commands.Handlers
{
    public class UsuarioCommandHandler : BaseCommandHandler<Usuario>,
        IRequestHandler<AdicionarUsuarioCommand, bool>,
        IRequestHandler<AdicionarEmpresaUsuarioCommand, bool>,
        IRequestHandler<AdicionarGrupoUsuarioCommand, bool>,
        IRequestHandler<AdicionarQuadraEmpresaUsuarioCommand, bool>
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMediatrHandler _mediatr;

        public UsuarioCommandHandler(IUsuarioRepository repository, IMediatrHandler mediatr) : base(repository, mediatr)
        {
            _repository = repository;
            _mediatr = mediatr;
        }

        public async Task<bool> Handle(AdicionarUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Usuario usuario = new Usuario(message.NomeUsuario, message.EmailUsuario, message.SenhaUsuario);

            _repository.AdicionarUsuario(usuario);

            return await SaveAndPublish(new UsuarioAdicionadoEvent(usuario.Id, usuario.Nome, usuario.Email, usuario.Senha));
        }

        public async Task<bool> Handle(AdicionarEmpresaUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Usuario proprietarioEmpresa = await _repository.ObterUsuarioPorId(message.UsuarioProprietarioId);

            if (proprietarioEmpresa == null) return await NotifyAndReturn("Usuário não encontrado.");

            Empresa novaEmpresa = new Empresa(message.UsuarioProprietarioId, message.RazaoSocial, message.Cnpj, message.HorarioAbertura, message.HorarioFechamento);

            proprietarioEmpresa.AdicionarEmpresa(novaEmpresa);

            return await SaveAndPublish(new EmpresaUsuarioAdicionadaEvent(message.UsuarioProprietarioId, message.RazaoSocial, message.Cnpj,
                                                                                    message.DiasFuncionamento, message.HorarioAbertura, message.HorarioFechamento));
        }

        public async Task<bool> Handle(AdicionarGrupoUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Usuario proprietarioGrupo = await _repository.ObterUsuarioPorId(message.UsuarioCriadorId);

            if (proprietarioGrupo == null) return await NotifyAndReturn("Usuário não encontrado.");

            Grupo novoGrupo = new Grupo(message.UsuarioCriadorId, message.NomeGrupo, message.NumeroMaximoMembros);

            proprietarioGrupo.AdicionarGrupo(novoGrupo);

            return await SaveAndPublish(new GrupoUsuarioAdicionadoEvent(novoGrupo.UsuarioCriadorId, novoGrupo.Id, novoGrupo.NomeGrupo, novoGrupo.NumeroMaximoMembros));
        }

        public async Task<bool> Handle(AdicionarQuadraEmpresaUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Usuario proprietarioEmpresa = await _repository.ObterUsuarioPorId(message.UsuarioId);

            if (proprietarioEmpresa == null) return await NotifyAndReturn("Usuário não encontrado.");

            Empresa empresa = proprietarioEmpresa.Empresas.SingleOrDefault(empresa => empresa.Id.Equals(message.EmpresaId));

            if (empresa == null) return await NotifyAndReturn("Empresa não encontrada.");

            Quadra novaQuadra = new Quadra(message.EmpresaId, message.TempoLocacao, message.ValorTempoLocado, message.TipoEsporteQuadra);
            empresa.AdicionarQuadra(novaQuadra);

            return await SaveAndPublish(new QuadraEmpresaUsuarioAdicionadaEvent(message.UsuarioId, novaQuadra.EmpresaId, novaQuadra.Id,
                                                            novaQuadra.TempoLocacao, novaQuadra.ValorTempoLocado, novaQuadra.TipoEsporteQuadra));
        }
    }
}
