using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Creational.Prototype
{
    public class Persona2
    {
        public string[] Names;
        public Address2 Address;

        public Persona2(string[] names, Address2 address)
        {
            Names = names;
            Address = address;
        }
        public Persona2(Persona2 other)
        {
            Names = (string[]) other.Names.Clone(); 
            Address = new Address2(other.Address);
        }
        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address2
    {
        public string StreetName;
        public int HouseNumber;

        public Address2(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }
        public Address2(Address2 other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }
        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class CopyConstructor : IRunner
    {
        public void Run()
        {
            var john = new Persona2(new[] { "John", "Doe" }, new Address2("Cork Road", 123));
            
            var jane = new Persona2(john);

            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 456;
            
            WriteLine(john);
            WriteLine(jane);
        }
    }
}
