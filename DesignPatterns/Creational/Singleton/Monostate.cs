using System;
using static System.Console;

namespace DesignPatterns.Creational.Singleton
{
    public class CEO
    {
        private static string _name;
        private static int _age;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }
    public class Monostate : IRunner
    {
        public void Run()
        {
            var ceo = new CEO();
            ceo.Name = "John Smith";
            ceo.Age = 55;

            var ceo2 = new CEO();
            WriteLine(ceo2);
        }
    }
}
