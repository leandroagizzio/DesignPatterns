using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns
{
    public class Person2
    {
        public string Name;
        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {

        }

        public static Builder New => new Builder();

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person2 person = new Person2();

        public Person2 Build() {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF> 
        : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
    {   
        public SELF Called(string name) {
            person.Name = name;
            return (SELF) this;
        }
    }

    public class PersonJobBuilder<SELF> 
        : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position) {
            person.Position = position;
            return (SELF) this;
        }
    }

    public class FluentBuilderInheritance : IRunner
    {
        public void Run() {

            //var builder = new PersonJobBuilder();
            //builder.Called("John").WorkAsA

            var person = Person2.New
                .Called("John")
                .WorksAsA("Tester")
                .Build();

            WriteLine(person);

        }
    }
}
