using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Decorator
{
    public class MyStringBuilder
    {
        private StringBuilder sb = new();

        public static implicit operator MyStringBuilder(string s)
        {
            var msb = new MyStringBuilder();
            msb.Append(s);
            return msb;
        }

        public static MyStringBuilder operator + (MyStringBuilder msb, string s)
        {
            msb.Append(s);
            return msb;
        }

        public MyStringBuilder Append(string s)
        {
            sb.Append(s);
            return this;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }

    public class AdapterDecorator : IRunner
    {
        public void Run()
        {
            MyStringBuilder s = "hello ";
            s += "world!";
            WriteLine(s);
            
        }
    }
}
