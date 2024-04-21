using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Entities
{
    public class EmailTemplate : MongoBaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
