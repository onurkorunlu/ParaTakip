using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Entities.Enums
{
    public static class BaseUserRoles
    {
        public static KeyValuePair<int, string> Admin = new KeyValuePair<int, string>(1, "Admin");
        public static KeyValuePair<int, string> Editor = new KeyValuePair<int, string>(2, "Editör");
        public static KeyValuePair<int, string> Kullanici = new KeyValuePair<int, string>(3, "Kullanıcı");
    }
}
