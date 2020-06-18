using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Data
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {

    }
}
