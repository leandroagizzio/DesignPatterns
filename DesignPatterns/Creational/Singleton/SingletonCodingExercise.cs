using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Singleton
{
    public class SingletonTester
    {        
        public static bool IsSingleton(Func<object> func)
        {
            var a = func();
            var b = func();
            return ReferenceEquals(a, b);
        }
    }
    public class SingletonCodingExercise : IRunner
    {
        public void Run()
        {
            
        }
    }
}
