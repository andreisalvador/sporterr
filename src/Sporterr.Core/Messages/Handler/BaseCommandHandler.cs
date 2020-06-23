using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Data;
using Sporterr.Core.DomainObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Core.Messages.Handler
{
    public class BaseCommandHandler<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        private readonly IRepository<TAggregateRoot> _repository;
        private readonly IMediatrHandler _mediatr;

        public BaseCommandHandler(IRepository<TAggregateRoot> repository, IMediatrHandler mediatr)
        {
            _repository = repository;
            _mediatr = mediatr;
        }

        protected async Task<bool> Salvar() => await _repository.Commit();
        protected async Task<bool> SalvarPublicandoEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            bool salvou = await Salvar();

            if (salvou) await _mediatr.Publish<TEvent>(evento);

            return salvou;
        }
    }
}
