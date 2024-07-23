using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Creational.Prototype
{
    public class Persona1 : ICloneable
    {
        public string Names;
        public Address1 Address;

        public Persona1(string names, Address1 address)
        {
            Names = names;
            Address = address;
        }

        public object Clone()
        {
            //return new Persona1((string[]) Names.Clone(), (Address1) Address.Clone());
            return new Persona1(Names, (Address1) Address.Clone());
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address1 : ICloneable
    {
        public string StreetName;
        public int HouseNumber;

        public Address1(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public object Clone()
        {
            return new Address1(StreetName, HouseNumber);
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class ICloneableIsBad : IRunner
    {
        public void Run()
        {
            //var john = new Persona1(new[] { "John", "Doe" }, new Address1("Cork Road", 123));
            var john = new Persona1("John Doe", new Address1("Cork Road", 123));
            var jane = (Persona1) john.Clone();

            jane.Names = "Jane Doe";
            jane.Address.HouseNumber = 456;
            
            WriteLine(john);
            WriteLine(jane);
        }
    }
}
