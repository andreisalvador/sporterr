using Sporterr.Core.Data;
using Sporterr.Sorteio.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Sorteio.Data.Repository.Interfaces
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
    }
}
