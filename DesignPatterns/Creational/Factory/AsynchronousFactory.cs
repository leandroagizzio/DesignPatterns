using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Factory
{
    public class Foo
    {
        private Foo() { }
        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync() { 
            var result = new Foo();
            return result.InitAsync();
        }
    }

    public class AsynchronousFactory : IRunner
    {
        public async void Run()
        {
            //var foo = new Foo();
            //await foo.InitAsync();  

            Foo x = await Foo.CreateAsync();
        }
    }
}
