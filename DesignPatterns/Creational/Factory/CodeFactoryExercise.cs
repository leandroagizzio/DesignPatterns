using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Factory
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonFactory
    {
        private int index;

        public PersonFactory()
        {
            index = 0;
        }

        public Person CreatePerson(string name)
        {
            return new Person { Id = index++, Name = name };
        }

    }
    public class CodeFactoryExercise : IRunner
    {
        public void Run()
        {
            var factory = new PersonFactory();
            
            Console.WriteLine(factory.CreatePerson("abc").Id);
            Console.WriteLine(factory.CreatePerson("def").Id);
            Console.WriteLine(factory.CreatePerson("ghi").Id);
        }
    }
}
