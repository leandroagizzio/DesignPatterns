using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Decorator
{
    public interface IBirdd
    {
        int Weight { get; set; }
        void Fly();
    }

    public class Birdd : IBirdd
    {
        public int Weight { get; set; }

        public void Fly()
        {
            WriteLine($"Soaring in the sky with weight {Weight}");
        }
    }

    public interface ILizardd
    {
        int Weight { get; set; }
        void Crawl();
    }

    public class Lizardd : ILizardd
    {
        public int Weight { get; set; }

        public void Crawl()
        {
            WriteLine($"Crawling in the dirt with weight {Weight}");
        }
    }

    public class Dragonn : IBirdd, ILizardd
    {
        private int _weight;
        private Birdd _birdd = new();
        private Lizardd _lizardd = new();

        //int ILizardd.Weight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //int IBirdd.Weight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Weight {
            get { return _weight; }
            set { 
                _weight = value;
                _birdd.Weight = value;
                _lizardd.Weight = value;
            }
        }

        public void Fly()
        {
            _birdd.Fly();
        }

        public void Crawl()
        {
            _lizardd.Crawl();
        }

    }

    public class MultipleInheritance : IRunner
    {
        public void Run()
        {
            var d = new Dragonn();
            d.Weight = 15;
            d.Fly();
            d.Crawl();
        }
    }
}
