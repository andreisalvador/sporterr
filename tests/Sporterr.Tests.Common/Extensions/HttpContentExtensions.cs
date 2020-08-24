using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Sporterr.Tests.Common.Extensions
{
    public static class HttpContentExtensions
    {
        public static StringContent ToStringContent(this object value)
            => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
    }
}
