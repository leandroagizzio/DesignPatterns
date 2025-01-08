using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Decorator
{
    public interface ICreature
    {
        int Age { get; set; }
    }
    public interface IBirb : ICreature
    {
        void Fly()
        {
            if (Age >= 10)
                WriteLine("I am Flying!");
        }
    }
    public interface ILizarz : ICreature
    {
        void Crawl()
        {
            if (Age < 10)
                WriteLine("I am Crawling!");
        }
    }

    public class Organism { }

    public class DDragon : Organism, /*ICreature,*/ IBirb, ILizarz
    {
        public int Age { get; set; }
    }

    // inheritance
    // SmartDragon(Dragon)
    // extension method
    // c#8 default interface methods

    public class MultipleInheritanceDefault : IRunner
    {
        public void Run()
        {
            var d = new DDragon { Age = 15 };
            
            if (d is IBirb bird)
                bird.Fly();
            if (d is  ILizarz lizarz)
                lizarz.Crawl();

        }
    }
}
