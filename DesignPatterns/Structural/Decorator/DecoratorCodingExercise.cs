using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.Decorator
{
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon // no need for interfaces
    {
        private Bird _birb = new();
        private Lizard _lizard = new();
        private int _age;

        public int Age
        {
            get { 
                return _age;
            }
            set { 
                _age = value;
                //_birb.Age = value;
                //_lizard.Age = value;
            }
        }

        public string Fly()
        {
            _birb.Age = Age;
            return _birb.Fly();
        }

        public string Crawl()
        {
            _lizard.Age = Age;
            return _lizard.Crawl();
        }
    }

    public class DecoratorCodingExercise : IRunner
    {
        public void Run()
        {

        }
    }
}
