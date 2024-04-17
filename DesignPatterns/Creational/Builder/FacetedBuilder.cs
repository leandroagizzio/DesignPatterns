using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Creational.Builder
{
    public class Person
    {
        // address
        public string StreetAddress, Postcode, City;

        //employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString() {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, " + 
                   $"{nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class PersonBuilderFacade
    {
        // reference!
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilderFacade pb) {
            return pb.person;
        }
    }

    public class PersonJobBuilder : PersonBuilderFacade
    {
        public PersonJobBuilder(Person person) {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName) {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position) {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount) {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilderFacade
    {
        // it does not work with a value type
        public PersonAddressBuilder(Person person) {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress) {
            person.StreetAddress = streetAddress;
            return this;
        }
        public PersonAddressBuilder WithPostcode(string postcode) {
            person.Postcode = postcode;
            return this;
        }
        public PersonAddressBuilder In(string city) {
            person.City = city;
            return this;
        }
    }

    public class FacetedBuilder : IRunner
    {
        public void Run() {
            var pb = new PersonBuilderFacade();
            Person person = pb
                .Lives.At("123 Road")
                      .In("Town city")
                      .WithPostcode("AB12CD")
                .Works.At("Fabrikam")
                      .AsA("Engineer")
                      .Earning(15);

            WriteLine(person);
        }
    }
}
