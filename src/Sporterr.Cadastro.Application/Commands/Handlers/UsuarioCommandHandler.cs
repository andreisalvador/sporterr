using MediatR;
using Sporterr.Cadastro.Application.Events;
using Sporterr.Cadastro.Data.Repository.Interfaces;
using Sporterr.Cadastro.Domain;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.DomainObjects.Exceptions;
using Sporterr.Core.Messages.Handler;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Application.Commands.Handlers
{
    public class UsuarioCommandHandler : BaseCommandHandler<Usuario>,
        IRequestHandler<AdicionarUsuarioCommand, bool>,
        IRequestHandler<AdicionarEmpresaUsuarioCommand, bool>,
        IRequestHandler<AdicionarGrupoUsuarioCommand, bool>,
        IRequestHandler<InativarEmpresaUsuarioCommand, bool>
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioCommandHandler(IUsuarioRepository repository, IMediatrHandler mediatr) : base(repository, mediatr)
        {
            _repository = repository;
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

            Empresa novaEmpresa = new Empresa(message.RazaoSocial, message.Cnpj, message.HorarioAbertura, message.HorarioFechamento);

            proprietarioEmpresa.AdicionarEmpresa(novaEmpresa);

            _repository.AdicionarEmpresa(novaEmpresa);

            return await SaveAndPublish(new EmpresaAdicionadaUsuarioEvent(novaEmpresa.Id, message.UsuarioProprietarioId, message.RazaoSocial, message.Cnpj,
                                                                                    message.DiasFuncionamento, message.HorarioAbertura, message.HorarioFechamento));
        }

        public async Task<bool> Handle(AdicionarGrupoUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Usuario proprietarioGrupo = await _repository.ObterUsuarioPorId(message.UsuarioCriadorId);

            if (proprietarioGrupo == null) return await NotifyAndReturn("Usuário não encontrado.");

            Grupo novoGrupo = new Grupo(message.NomeGrupo, message.NumeroMaximoMembros);

            proprietarioGrupo.AdicionarGrupo(novoGrupo);

            _repository.AdicionarGrupo(novoGrupo);

            _repository.AtualizarUsuario(proprietarioGrupo);

            return await SaveAndPublish(new GrupoAdicionadoUsuarioEvent(novoGrupo.UsuarioCriadorId, novoGrupo.Id, novoGrupo.NomeGrupo, novoGrupo.NumeroMaximoMembros));
        }

        public async Task<bool> Handle(InativarEmpresaUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return false;

            Usuario usuarioProprietarioEmpresa = await _repository.ObterUsuarioPorId(message.UsuarioProprietarioEmpresaId);

            if (usuarioProprietarioEmpresa == null) return await NotifyAndReturn("Usuário não encontrado.");

            Empresa empresaParaInativar = await _repository.ObterEmpresaPorId(message.EmpresaId);

            if (empresaParaInativar == null) return await NotifyAndReturn("Empresa não encontrada.");

            try
            {
                usuarioProprietarioEmpresa.InativarEmpresa(empresaParaInativar);
                _repository.AtualizarEmpresa(empresaParaInativar);
                _repository.AtualizarUsuario(usuarioProprietarioEmpresa);
            }
            catch (DomainException exception)
            {
                return await NotifyAndReturn(exception.Message);
            }

            return await SaveAndPublish(new EmpresaInativadaEvent(empresaParaInativar.Id, empresaParaInativar.UsuarioProprietarioId));
        }
    }
}
