using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Singleton
{
    public sealed class PerThread
    {
        private static ThreadLocal<PerThread> _threadInstance = new(
            () => new PerThread()
        );
        public int Id;
        private PerThread()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }
        public static PerThread ThreadInstance => _threadInstance.Value;
    }
    public class PerThreadSingleton : IRunner
    {
        public void Run()
        {
            var t1 = Task.Factory.StartNew( () => {
                Console.WriteLine("t1: " + PerThread.ThreadInstance.Id);
            });
            var t2 = Task.Factory.StartNew( () => {
                Console.WriteLine("t2: " + PerThread.ThreadInstance.Id);
                Console.WriteLine("t2: " + PerThread.ThreadInstance.Id);
            });
            Task.WaitAll(t1, t2);
        }
    }
}
