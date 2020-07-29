using MediatR;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Data;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Core.Services
{
    public abstract class DomainService<TEntity, TRepository> : IDomainService<TEntity> where TEntity : IAggregateRoot
                                                                                        where TRepository : IRepository<TEntity>
    {
        private readonly IMediatrHandler _mediatr;

        protected TRepository Repository { get; }
        protected DomainService(IRepository<TEntity> repository, IMediatrHandler mediatr)
        {
            Repository = (TRepository)repository;
            _mediatr = mediatr;
        }

        public async Task Notify(string message)
        {
            await _mediatr.Notify(new DomainNotification(this.GetType().Name, message));
        }
    }
}
