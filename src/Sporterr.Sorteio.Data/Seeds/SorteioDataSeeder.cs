using Sporterr.Core.Data;
using Sporterr.Sorteio.Domain;
using Sporterr.Sorteio.Domain.Data.Interfaces;
using Sporterr.Sorteio.Domain.Esportes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Data.Seeds
{
    public class SorteioDataSeeder : IDataSeeder<SorteioContext>
    {
        private readonly IEsporteRepository _esporteRepository;
        private readonly SorteioContext _context;

        public SorteioDataSeeder(SorteioContext context, IEsporteRepository esporteRepository)
        {
            _context = context;
            _esporteRepository = esporteRepository;
        }

        public async Task Seed()
        {
            if (!_context.Esportes.Any())
            {
                IEnumerable<Esporte> esportesPadroes = new Esporte[]
                {
                    new Futebol()
                };

                foreach (Esporte esporte in esportesPadroes)
                {
                    _esporteRepository.AdicionarHabilidades(esporte.Habilidades);
                    _esporteRepository.AdicionarEsporte(esporte);
                }

                await _esporteRepository.Commit();
            }
        }
    }
}
