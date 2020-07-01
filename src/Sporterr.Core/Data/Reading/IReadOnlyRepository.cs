using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Core.Data.Reading
{
    public interface IReadOnlyRepository
    {
        Task InsertOrUpdate();
    }
}
