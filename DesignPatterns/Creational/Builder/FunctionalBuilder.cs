using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Creational.Builder
{
    public class Person3
    {
        public string Name, Position;
    }

    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<Person3, Person3>> actions = new List<Func<Person3, Person3>>();

        private TSelf AddAction(Action<Person3> action) {
            actions.Add(p => {
                action(p);
                return p;
            });
            return (TSelf) this;
        }

        public TSelf Do(Action<Person3> action)
            => AddAction(action);

        public Person3 Build()
            => actions.Aggregate(new Person3(), (p, f) => f(p));
    }

    public sealed class PersonBuilder2 
        : FunctionalBuilder<Person3, PersonBuilder2>
    {
        public PersonBuilder2 Called(string name)
            => Do(p => p.Name = name);
    }

    //public sealed class PersonBuilder2
    //{
    //    private readonly List<Func<Person3, Person3>> actions = new List<Func<Person3, Person3>>();

    //    private PersonBuilder2 AddAction(Action<Person3> action) {
    //        actions.Add(p => {
    //            action(p);
    //            return p;
    //        });
    //        return this;
    //    }

    //    public PersonBuilder2 Do(Action<Person3> action) 
    //        => AddAction(action);

    //    public PersonBuilder2 Called(string name) 
    //        => Do(p => p.Name = name);

    //    public Person3 Build()
    //        => actions.Aggregate(new Person3(), (p, f) => f(p));
    //}

    public static class PersonBuilderExtensions 
    {
        public static PersonBuilder2 WorksAs
            (this PersonBuilder2 builder, string position)
            => builder.Do(p => p.Position = position);
    }

    public class FunctionalBuilder : IRunner
    {
        public void Run() {
            var person = new PersonBuilder2()
                .Called("John Doe")
                .WorksAs("Developer")
                .Build();
        }
    }
}
