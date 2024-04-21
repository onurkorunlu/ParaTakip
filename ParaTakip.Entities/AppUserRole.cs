using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Entities
{
    public class AppUserRole : MongoBaseEntity
    {
        public string RoleName { get; set; }
    }
}
