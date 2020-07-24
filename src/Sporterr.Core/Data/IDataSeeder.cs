using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Core.Data
{
    public interface IDataSeeder<TContext> where TContext : IDbContext
    {
        Task Seed();
    }
}
