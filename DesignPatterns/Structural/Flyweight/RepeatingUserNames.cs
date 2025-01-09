using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Kernel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Flyweight
{
    public class User
    {
        private string _fullName;

        public User(string fullName)
        {
            _fullName = fullName;
        }
    }

    public class User2
    {
        public static List<string> strings = new();
        private int[] names;

        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName() => string.Join(' ',
            names.Select(i => strings[i])
        );
    }

    [TestFixture]
    public class RepeatingUserNames : IRunner
    {
        public void Run()
        {
            TestUser2();
        }

        [Test]
        public void TestUser() // 8 450 715
        {

            var firstNames = Enumerable.Range(0, 500).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 500).Select(_ => RandomString());

            var users = new List<User>();

            foreach (var firstName in firstNames)
                //foreach (var lastName in lastNames)
            foreach (var lastName in firstNames)
                    users.Add(new User($"{firstName} {lastName}"));

            ForceGC();

            dotMemory.Check(memory =>
            {
                WriteLine(memory.SizeInBytes);
            });

        }

        [Test]
        public void TestUser2() 
        {

            var firstNames = Enumerable.Range(0, 500).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 500).Select(_ => RandomString());

            var users = new List<User2>();

            foreach (var firstName in firstNames)
            //foreach (var lastName in lastNames)
            foreach (var lastName in firstNames)
                        users.Add(new User2($"{firstName} {lastName}"));

            ForceGC();

            dotMemory.Check(memory =>
            {
                WriteLine(memory.SizeInBytes);
            });

        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string RandomString()
        {
            Random rand = new Random();
            //return new string(
            //    Enumerable.Range(0, 10)
            //    .Select(i => (char)('a' + rand.Next(26)))
            //    .ToArray()
            //);
            return "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        }

    }
}
