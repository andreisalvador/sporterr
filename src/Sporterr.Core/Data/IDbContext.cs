using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Core.Data
{
    public interface IDbContext
    {
        Task<bool> CommitAsync();
    }
}
