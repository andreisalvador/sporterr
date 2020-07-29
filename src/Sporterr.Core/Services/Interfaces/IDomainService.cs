using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Core.Services.Interfaces
{
    public interface IDomainService<TEntity> where TEntity : IAggregateRoot
    {
        Task Notify(string message);
    }
}
