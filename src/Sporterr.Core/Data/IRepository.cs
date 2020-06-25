using Sporterr.Core.DomainObjects.Interfaces;
using System.Threading.Tasks;

namespace Sporterr.Core.Data
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        Task<bool> Commit();
    }
}
