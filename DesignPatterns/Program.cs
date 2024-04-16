using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.SOLID;
using DesignPatterns.Creational.Builder;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args) {

            IRunner runner = new FunctionalBuilder();
            runner.Run();

            Console.ReadKey();
        }
    }
}
