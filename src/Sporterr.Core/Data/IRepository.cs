using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sporterr.Core.Data
{
    public interface IRepository<TAggregateRoot> : IDisposable where TAggregateRoot : IAggregateRoot
    {
        Task<bool> Commit();
    }
}
