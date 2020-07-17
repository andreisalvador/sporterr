using Sporterr.Core.DomainObjects;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporterr.Sorteio.Domain
{
    public class PerfilHabilidades : Entity<PerfilHabilidades>, IAggregateRoot, IActivationEntity
    {
        private readonly List<HabilidadeUsuario> _habilidadesUsuario;

        public Guid UsuarioId { get; private set; }
        public TipoEsporte EsportesUsuario { get; private set; }
        public IReadOnlyCollection<HabilidadeUsuario> HabilidadesUsario => _habilidadesUsuario.AsReadOnly();
        public bool Ativo { get; private set; }

        public PerfilHabilidades(Guid usuarioId)
        {
            UsuarioId = usuarioId;            
            _habilidadesUsuario = new List<HabilidadeUsuario>();
            Ativar();
        }

        public void AdicionarHabilidadesUsuario(IEnumerable<HabilidadeUsuario> habilidadesUsuario)
        {
            foreach (HabilidadeUsuario habilidade in habilidadesUsuario)
                AdicionarHabilidadeUsuario(habilidade);
        }   

        public void AdicionarHabilidadeUsuario(HabilidadeUsuario habilidadeUsuario)
        {
            habilidadeUsuario.AssociarPerfilHabilidadesUsuario(Id);
            _habilidadesUsuario.Add(habilidadeUsuario);            
        }

        public IEnumerable<HabilidadeUsuario> ObterHabilidadesPorEsporte(TipoEsporte esporte)
            => _habilidadesUsuario.Where(h => h.Esporte.Equals(esporte));

        public void VincularNovoEsporte(TipoEsporte esporte)
        {
            if (!EsportesUsuario.HasFlag(esporte)) EsportesUsuario |= esporte;
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}
