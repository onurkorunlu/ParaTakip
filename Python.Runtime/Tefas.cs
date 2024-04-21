using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Python.Runtime
{
    public static class Tefas
    {
        public static void GetFundPrice()
        {
            using (Py.GIL())
            {
                // NOTE: this doesn't validate input
                Console.WriteLine("Enter first integer:");
                var firstInt = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter second integer:");
                var secondInt = int.Parse(Console.ReadLine());

                using dynamic scope = Py.CreateScope();
                scope.Exec("def add(a, b): return a + b");
                var sum = scope.add(firstInt, secondInt);
                Console.WriteLine($"Sum: {sum}");
            }
        }
    }

}
