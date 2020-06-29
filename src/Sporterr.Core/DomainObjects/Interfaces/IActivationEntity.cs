using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.DomainObjects.Interfaces
{
    public interface IActivationEntity
    {
        bool Ativo { get; }        

        void Ativar();
        void Inativar();
    }
}
