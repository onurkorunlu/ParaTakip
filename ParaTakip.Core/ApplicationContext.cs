using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Core
{
    public class ApplicationContext : IApplicationContext
    {
        public readonly IHttpContextAccessor Current;

        public ApplicationContext(IHttpContextAccessor httpContextAccessor)
        {
            this.Current = httpContextAccessor;
        }
    }
}
