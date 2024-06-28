using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.SOLID;
using DesignPatterns.Creational.Builder;
using DesignPatterns.Creational.Factory;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {

            IRunner runner = new CodeFactoryExercise();
            runner.Run();

            Console.ReadKey();
        }
    }
}
