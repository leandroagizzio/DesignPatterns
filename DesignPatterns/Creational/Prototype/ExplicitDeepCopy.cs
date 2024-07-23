using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Creational.Prototype
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }
    public class Persona3 : IPrototype<Persona3>
    {
        public string Names;
        public Address3 Address;

        public Persona3(string names, Address3 address)
        {
            Names = names;
            Address = address;
        }
        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
        public Persona3 DeepCopy()
        {
            return new Persona3 (Names, Address.DeepCopy());
        }
    }

    public class Address3 : IPrototype<Address3> 
    {
        public string StreetName;
        public int HouseNumber;

        public Address3(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }
        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public Address3 DeepCopy()
        {
            return new Address3 (StreetName, HouseNumber);
        }
    }

    public class ExplicitDeepCopy : IRunner
    {
        public void Run()
        {
            var john = new Persona3("John Doe", new Address3("Cork Road", 123));

            var jane = john.DeepCopy();

            jane.Names = "Jane Smith";
            jane.Address.HouseNumber = 456;

            WriteLine(john);
            WriteLine(jane);
        }
    }

}
