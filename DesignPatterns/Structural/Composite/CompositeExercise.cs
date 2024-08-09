using System;
using System.Collections;
using static System.Console;

namespace DesignPatterns.Structural.Composite
{
    public interface IValueContainer : IEnumerable<int>
    {

    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {

    }

    public static class ExtensionMethods2
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }

    public class CompositeExercise : IRunner
    {
        public void Run()
        {
            var s = new SingleValue { Value = 15 };
            WriteLine($"single: {s.Sum()}");

            var m = new ManyValues();
            m.Add(15);
            WriteLine($"1st many: {m.Sum()}");

            m.Add(12);
            m.Add(20);
            WriteLine($"2st many: {m.Sum()}");
        }
    }
}
