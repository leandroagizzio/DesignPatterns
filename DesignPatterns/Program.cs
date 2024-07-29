using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.SOLID;
using DesignPatterns.Creational.Builder;
using DesignPatterns.Creational.Factory;
using DesignPatterns.Creational.Prototype;
using DesignPatterns.Creational.Singleton;
using DesignPatterns.Structural.Adapter;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {

            IRunner runner = new AdapterCodingExercise();
            runner.Run();

            Console.ReadKey();
        }

    }
}
