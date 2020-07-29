using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Services;
using Sporterr.Sorteio.Domain.Data.Interfaces;
using Sporterr.Sorteio.Domain.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Domain.Services
{
    public class PerfilServices : DomainService<PerfilHabilidades, IPerfilHabilidadesRepository>, IPerfilServices
    {
        private readonly IEsporteRepository _esporteRepository;
        private readonly IHabilidadeUsuarioRepository _habilidadeUsuarioRepository;

        public PerfilServices(IEsporteRepository esporteRepository,
            IHabilidadeUsuarioRepository habilidadeUsuarioRepository,
            IPerfilHabilidadesRepository perfilHabilidadesRepository,
            IMediatrHandler mediatr) : base(perfilHabilidadesRepository, mediatr)
        {
            _esporteRepository = esporteRepository;
            _habilidadeUsuarioRepository = habilidadeUsuarioRepository;
        }

        public async Task AdicionarNovoEsporte(Guid perfilId, Guid esporteId)
        {
            PerfilHabilidades perfil = await Repository.ObterPorId(perfilId);

            if (perfil == null)
                await Notify("O perfil informado não foi encontrado na base de dados.");

            Esporte esporte = await _esporteRepository.ObterEsporteComHabilidadesPorId(esporteId);

            if (esporte == null)
                await Notify("O esporte informado não foi encontrado na base de dados.");

            if (perfil != null && esporte != null)
            {
                perfil.VincularNovoEsporte(esporte.TipoEsporte);

                IList<HabilidadeUsuario> novasHabilidadesUsuario = new List<HabilidadeUsuario>();

                foreach (Habilidade habilidade in esporte.Habilidades)
                    novasHabilidadesUsuario.Add(habilidade);

                perfil.AdicionarHabilidadesUsuario(novasHabilidadesUsuario);

                _habilidadeUsuarioRepository.AdicionarHabilidadesUsuario(novasHabilidadesUsuario);
                Repository.AtualizarPerfilHabilidades(perfil);

                await _habilidadeUsuarioRepository.Commit();
                await Repository.Commit();
            }
        }

        public async Task AvaliarPerfil(Guid perfilId, IDictionary<Guid, double> habilidadesAvaliadas)
        {
            PerfilHabilidades perfil = await Repository.ObterPorIdComHabilidades(perfilId);

            if (perfil == null)
                await Notify("O perfil informado não foi encontrado na base de dados.");

            if (perfil != null)
            {
                IEnumerable<HabilidadeUsuario> habilidadesParaAvaliar = perfil.HabilidadesUsuario
                                                                              .Where(h => habilidadesAvaliadas.Select(h => h.Key).Contains(h.Id));

                foreach (HabilidadeUsuario habilidade in habilidadesParaAvaliar)
                {
                    AvaliacaoHabilidade avaliacao = new AvaliacaoHabilidade(perfil.UsuarioId, habilidadesAvaliadas[habilidade.Id]);
                    habilidade.AdicionarAvaliacaoHabilidade(avaliacao);
                    _habilidadeUsuarioRepository.AdicionarAvaliacaoHabilidade(avaliacao);
                }

                _habilidadeUsuarioRepository.AtualizarHabilidadesUsuario(habilidadesParaAvaliar);
                Repository.AtualizarPerfilHabilidades(perfil);

                await _habilidadeUsuarioRepository.Commit();
                await Repository.Commit();
            }
        }

        public Task RemoverEsporte(Guid perfilId, Guid esporteId)
        {
            throw new NotImplementedException();
        }
    }
}
