using Sporterr.Core.Data;
using Sporterr.Sorteio.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Sorteio.Domain.Data.Interfaces
{
    public interface IHabilidadeUsuarioRepository : IRepository<HabilidadeUsuario>
    {
        void AdicionarHabilidadeUsuario(HabilidadeUsuario habilidadeUsuario);
        void AdicionarHabilidadesUsuario(IEnumerable<HabilidadeUsuario> habilidadesUsuario);
        void AtualizarHabilidadeUsuario(HabilidadeUsuario habilidadeUsuario);
        void AtualizarHabilidadesUsuario(IEnumerable<HabilidadeUsuario> habilidadesUsuario);
        void RemoverHabilidadeUsuario(HabilidadeUsuario habilidadeUsuario);
        void RemoverHabilidadesUsuario(IEnumerable<HabilidadeUsuario> habilidadesUsuario);

        void AdicionarAvaliacaoHabilidade(AvaliacaoHabilidade avaliacaoHabilidade);
        void AdicionarAvaliacoesHabilidade(IEnumerable<AvaliacaoHabilidade> avaliacaoHabilidade);

        Task<HabilidadeUsuario> ObterPorId(Guid habilidadeUsuarioId);
        Task<AvaliacaoHabilidade> ObterAvaliacaoHabilidadePorId(Guid avaliacaoHabilidadeId);
    }
}
