using Autofac;
using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Creational.Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string city);
    }    
    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> _capitals;
        private static int _instanceCount; // 0 by default
        public static int Count => _instanceCount;
        private SingletonDatabase()
        {
            _instanceCount++;
            WriteLine("Initializing database");

            _capitals = File.ReadAllLines(
                    Path.Combine(
                        new FileInfo(typeof(SingletonImplementation).Assembly.Location).DirectoryName,
                        "Creational/Singleton/capitals.txt"
                    )
                )
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
        }
        public int GetPopulation(string city)
        {
            return _capitals[city];
        }
        //private static SingletonDatabase _instance = new();
        private static Lazy<SingletonDatabase> _instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        //public static SingletonDatabase Instance => _instance;
        public static SingletonDatabase Instance => _instance.Value;
    }
    public class OrdinaryDatabase : IDatabase
    {
        private Dictionary<string, int> _capitals;
        public OrdinaryDatabase()
        {
            WriteLine("Initializing database");

            _capitals = File.ReadAllLines("Creational/Singleton/capitals.txt")
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
        }
        public int GetPopulation(string city)
        {
            return _capitals[city];
        }
    }
    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
                result += SingletonDatabase.Instance.GetPopulation(name);
            return result;
        }
    }

    public class ConfigurableRecordFinder
    {
        private IDatabase _database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(paramName: nameof(database));
        }
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
                result += _database.GetPopulation(name);
            return result;
        }
    }

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string city)
        {
            return new Dictionary<string, int> {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[city];
        }
    }

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }
        [Test]
        public void SingletonTotalPolulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] { "Seoul", "Mexico City" };
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000 + 17400000));
        }
        [Test]
        public void ConfigurablePopulationTest()
        {
            var rf = new ConfigurableRecordFinder(new DummyDatabase());
            var names = new[] { "alpha", "gamma" };
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(1 + 3));
        }
        [Test]
        public void DIPopulationTest()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<OrdinaryDatabase>().As<IDatabase>().SingleInstance();
            cb.RegisterType<ConfigurableRecordFinder>();
            using (var c = cb.Build())
            {
                var rf = c.Resolve<ConfigurableRecordFinder>();
            }
        }
    }

    public class SingletonImplementation : IRunner
    {
        public void Run()
        {
            var db = SingletonDatabase.Instance;
            
            var city = "Osaka";
            
            WriteLine($"{city}: {db.GetPopulation(city)}");
        }
    }
}
