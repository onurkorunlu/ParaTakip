using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Python.Runtime;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace ParaTakip.PythonIntegrator
{
    public static class Tefas
    {
        public static decimal GetFundPrice(string fundCode)
        {

            var code = File.ReadAllText("py/tefas.py");
            Runtime.PythonDLL = "python38.dll";
            PythonEngine.Initialize();
            using (Py.GIL())
            {
                using dynamic scope = Py.CreateScope();
                scope.Set("fonkod", fundCode);
                scope.Exec(code);
                var a = scope.Get<string>("tarih");
                string b = scope.Get<object>("rakam");

                var rakamList = b.Split(',');
                return decimal.Parse(rakamList.Last());
            }
        }
    }
}
