﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args) {

            IRunner runner = new SingleResponsibility();
            runner.Run();

            Console.ReadKey();
        }
    }
}
