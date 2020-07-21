using Sporterr.Sorteio.Domain.Data.Interfaces;
using Sporterr.Sorteio.Domain.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Domain.Services
{
    public class PerfilServices : IPerfilServices
    {
        private readonly IEsporteRepository _esporteRepository;
        private readonly IHabilidadeUsuarioRepository _habilidadeUsuarioRepository;
        private readonly IPerfilHabilidadesRepository _perfilHabilidadesRepository;

        public PerfilServices(IEsporteRepository esporteRepository, IHabilidadeUsuarioRepository habilidadeUsuarioRepository, IPerfilHabilidadesRepository perfilHabilidadesRepository)
        {
            _esporteRepository = esporteRepository;
            _habilidadeUsuarioRepository = habilidadeUsuarioRepository;
            _perfilHabilidadesRepository = perfilHabilidadesRepository;
        }

        public async Task AdicionarNovoEsporte(Guid perfilId, Guid esporteId)
        {
            PerfilHabilidades perfil = await _perfilHabilidadesRepository.ObterPorId(perfilId);
            Esporte esporte = await _esporteRepository.ObterEsportePorId(esporteId);

            perfil.VincularNovoEsporte(esporte.TipoEsporte);

            IList<HabilidadeUsuario> novasHabilidadesUsuario = new List<HabilidadeUsuario>();

            foreach (Habilidade habilidade in esporte.Habilidades)
                novasHabilidadesUsuario.Add(habilidade);

            _habilidadeUsuarioRepository.AdicionarHabilidadesUsuario(novasHabilidadesUsuario);
            _perfilHabilidadesRepository.AtualizarPerfilHabilidades(perfil);

            await _habilidadeUsuarioRepository.Commit();
            await _perfilHabilidadesRepository.Commit();
        }

        public Task RemoverEsporte(Guid perfilId, Guid esporteId)
        {
            throw new NotImplementedException();
        }
    }
}
