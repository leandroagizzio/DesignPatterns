using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.Proxy
{
    public class Person
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson
    {
        private Person _person;

        public ResponsiblePerson(Person person)
        {
            // todo
            _person = person;
        }

        public string Drink()
        {
            if (Age < 18)
                return "too young";
            return _person.Drink();
        }

        public string Drive()
        {
            if (Age < 16)
                return "too young";
            return _person.Drive();
        }

        public string DrinkAndDrive()
        {
            return "not good idea";
        }

        public int Age { /* todo implement property */ 
            get {
                return _person.Age;
            }
            set {
                _person.Age = value;
            }
        }
    }
    public class ProxyCodingExercise : IRunner
    {
        public void Run()
        {
            
        }
    }
}
