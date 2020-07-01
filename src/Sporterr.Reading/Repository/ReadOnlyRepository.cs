using Sporterr.Core.Data.Reading;
using System;
using System.Threading.Tasks;

namespace Sporterr.Reading.Repository
{
    public class ReadOnlyRepository : IReadOnlyRepository
    {
        public Task InsertOrUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
