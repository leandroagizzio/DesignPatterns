using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Proxy 
{    
    public class PropertyProxy : IRunner 
    {

        public void Run() {
            var c = new Creature();
            c.Agility = 10;
        }

        public class Creature 
        {
            private Property<int> _agility { get; set; }
            public int Agility {
                get => _agility.Value;
                set => _agility.Value = value;
            }
        }

        public class Property<T> where T : new() 
        {

            private T _value;

            public T Value {
                get => _value; 
                set {
                    if (Equals(_value, value)) return;
                    WriteLine($"Assigning value to {value}");
                    _value = value;
                }
            }

            public Property() : this(Activator.CreateInstance<T>()) {

            }

            public Property(T value) {
                _value = value;
            }

            // int n = property_int;
            public static implicit operator T(Property<T> property) {
                return property.Value;
            }

            //Property<int> p = 123;
            public static implicit operator Property<T>(T value) {
                return new Property<T>(value);
            }

            public bool Equals(Property<T> other) {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return EqualityComparer<T>.Default.Equals(_value, other.Value);
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Property<T>)obj);
            }

            public override int GetHashCode() {
                return _value.GetHashCode();
            }

            public static bool operator ==(Property<T> left, Property<T> right) {
                return Equals(left, right);
            }

            public static bool operator !=(Property<T> left, Property<T> right) {
                return !Equals(left, right);
            }

        }



    }

}
