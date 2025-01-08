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
using DesignPatterns.Structural.Bridge;
using DesignPatterns.Structural.Composite;
using DesignPatterns.Structural.Decorator;
using DesignPatterns.Structural.Flyweight;
using DesignPatterns.Structural.Proxy;
using DesignPatterns.Behavioural.ChainOfResponsibility;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {

            IRunner runner = new DecoratorInDepInj();
            runner.Run();

            Console.ReadKey();
        }

    }
}
