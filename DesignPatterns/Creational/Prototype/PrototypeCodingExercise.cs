using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Creational.Prototype
{
    public class Point
    {
        public int X, Y;
    }

    public class Line
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            var s = new Point { X = Start.X, Y = Start.Y };
            var e = new Point { X = End.X, Y = End.Y };
            return new Line { Start = s, End = e };
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start.X}, {Start.Y} - {nameof(End)}: {End.X}, {End.Y}";
        }
    }

        public class PrototypeCodingExercise : IRunner
    {
        public void Run()
        {
            var a = new Line { 
                Start = new Point { X = 10, Y = 15 }, 
                End = new Point { X = 20, Y = 25 } 
            };

            var b = a.DeepCopy();
            b.Start.X = 11;
            b.End.Y = 31;

            WriteLine(a);
            WriteLine(b);
        }
    }
}
