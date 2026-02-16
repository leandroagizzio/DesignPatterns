using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static DesignPatterns.Structural.Proxy.ValueProxy;
using static System.Console;

namespace DesignPatterns.Structural.Proxy
{
    public static class PercentageExtensions
    {
        public static Percentage<T> Percent<T>(this int value) where T : IFloatingPoint<T> {
            T val = T.CreateChecked(value); // create the T instance of that value
            T per = T.CreateChecked(100);
            return new Percentage<T>(val / per);
        }
    }

    public class ValueProxy : IRunner
    {
        public void Run() {
            //var x = new Percentage<float>();
            
            WriteLine(10f * 5.Percent<float>());
            WriteLine(2.Percent<double>() + 3.Percent<double>());
        }

        [DebuggerDisplay("{_value*100}%")]
        public struct Percentage<T> where T : IFloatingPoint<T> // IFloatingPoint points to decimal primitive types (float, double, ...) 
        {
            private readonly T _value;

            internal Percentage(T value) {
                _value = value;
            }

            public static T operator *(T t, Percentage<T> p) {
                return t * p._value;
            }

            public static Percentage<T> operator +(Percentage<T> a, Percentage<T> b) {
                return new Percentage<T>(a._value + b._value);
            }

            public override string ToString() {
                return $"{_value * T.CreateChecked(100)}%";
            }
        }


        public void foo (Price price) {

        }

        public struct Price 
        {
            private int _value;

            public Price(int value) {
                _value = value;
            }
        }

    }

}
