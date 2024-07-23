using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using static System.Console;

namespace DesignPatterns.Creational.Prototype
{
    public static class ExtensionMethods2
    {
        public static T DeepCopy2<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter(); // must add xml property to ignore warning because BinaryFormatter is obsolete
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }

        public static T DeepCopyXML<T>(this T self)
        {
            using var ms = new MemoryStream();
            var s = new XmlSerializer(typeof(T));
            s.Serialize(ms, self);
            ms.Position = 0;
            return (T) s.Deserialize(ms);

        }
    }

    //for BinaryFormatter
    //[Serializable]
    public class Persona4
    {
        public string[] Names;
        public Address4 Address;
        public Persona4() { }
        public Persona4(string[] names, Address4 address)
        {
            Names = names;
            Address = address;
        }
        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    //[Serializable]
    public class Address4
    {
        public string StreetName;
        public int HouseNumber;
        public Address4() { }
        public Address4(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }
        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class CopySerialization : IRunner
    {
        public void Run()
        {
            var john = new Persona4(new[] { "John", "Storm" }, new Address4("Cork Road", 123));

            //var jane = john.DeepCopy2();
            var jane = john.DeepCopyXML();

            jane.Names[0] = "Janet";
            jane.Address.HouseNumber = 789;

            WriteLine(john);
            WriteLine(jane);
        }
    }

}
