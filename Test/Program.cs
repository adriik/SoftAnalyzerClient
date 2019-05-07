using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        public static double zmienna = 0.0f;
        static void Main(string[] args)
        {
            Random random = new Random();
            zmienna = random.NextDouble();
            Console.WriteLine("Hello World!" + zmienna);
        }
    }
}
