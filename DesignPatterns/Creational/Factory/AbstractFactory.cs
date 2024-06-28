using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Creational.Factory
{
    public interface IHotDrink
    {
        void Consume();
    }
    public class Tea : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This Tea is nice.");
        }
    }
    public class Coffee : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This Coffee is good.");
        }
    }
    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }
    public class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Put the tea bag, boil water, pour {amount} ml, enjoy.");
            return new Tea();
        }
    }
    public class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Grind beans, boil water, pour {amount} ml, enjoy.");
            return new Coffee();
        }
    }
    public class HotDrinkMachine
    {
        //public enum AvailableDrink
        //{
        //    Coffee, Tea
        //}

        //private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new();

        //public HotDrinkMachine()
        //{
        //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //    {
        //        var factory = (IHotDrinkFactory) 
        //            Activator.CreateInstance(
        //                Type.GetType("DesignPatterns.Creational.Factory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")             
        //            );

        //        factories.Add(drink, factory);
        //    }
        //}

        //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        //{
        //    return factories[drink].Prepare(amount);
        //}

        private List<Tuple<string, IHotDrinkFactory>> factories = new();

        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                // test if implements one interface                 // not an interface (or self)
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    factories.Add(
                            Tuple.Create(
                                t.Name.Replace("Factory", string.Empty), (IHotDrinkFactory) Activator.CreateInstance(t)
                            )
                    );
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            WriteLine("Available drinks: ");
            for (int index = 0; index < factories.Count; index++)
            {
                var tuple = factories[index];
                WriteLine($"{index} - {tuple.Item1}");
            }

            while (true)
            {
                string s;
                if (
                    (s = Console.ReadLine()) != null 
                    && int.TryParse(s, out int i)
                    && i >= 0
                    && i < factories.Count
                    ) 
                {
                    Write("Specify the amount: ");
                    s = ReadLine();
                    if (s != null
                        && int.TryParse(s, out int amount)
                        && amount > 0)
                    {
                        return factories[i].Item2.Prepare(amount);
                    }
                }

                WriteLine("Incorrect input");
            }
        }
    }

    public class AbstractFactory : IRunner
    {
        public void Run()
        {
            var machine = new HotDrinkMachine();
            //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 150);
            //drink.Consume();

            var drink = machine.MakeDrink();
            drink.Consume();
        }
    }
}
