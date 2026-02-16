using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Proxy 
{
    public class ProtectionProxy : IRunner 
    {
        public void Run() {
            ICar car = new CarProxy(new Driver { Age = 22 });
            car.Drive();

        }

        public interface ICar {
            void Drive();
        }

        public class Car : ICar 
        {
            public void Drive() {
                WriteLine("Car is being driven");
            }
        }

        public class Driver {
            public int Age { get; set; }
        }

        public class CarProxy : ICar 
        {
            private Driver _driver;
            private Car _car = new Car();

            public CarProxy(Driver driver) {
                _driver = driver;
            }

            public void Drive() {
                if (_driver.Age >= 16)
                    _car.Drive();
                else
                    WriteLine("Too young!!!");

            }
        }
    }

}
