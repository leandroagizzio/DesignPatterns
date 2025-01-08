using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace DesignPatterns.Structural.Decorator
{
    public class CodeBuilder
    {
        private StringBuilder builder = new();

        public CodeBuilder Clear()
        {
            builder.Clear();
            return this;
        }

        public CodeBuilder Append(string text)
        {
            builder.Append(text);
            return this;
        }
        public override string ToString()
        {
            return builder.ToString();
        }
    }

    public class CustomStringBuilder : IRunner
    {
        public void Run()
        {
            var cb = new CodeBuilder();
            cb.Append("abc").Append("123").Append("zxc");
            WriteLine($"cb: '{cb}'");
            cb.Clear();
            WriteLine($"cb: '{cb}'");
            

        }
    }
}
