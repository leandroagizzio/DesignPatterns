using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Creational.Builder
{
    public class CodeBuilder
    {
        private string className;
        private List<Property> properties;

        public class Property
        {
            public string Prop, Type;
        }
        public CodeBuilder(string name) {
            className = name;
            properties = new List<Property>();
        }

        public CodeBuilder AddField(string property, string type) {
            properties.Add(new Property { Prop = property, Type = type } );
            return this;
        }

        public override string ToString() {
            string ret = $"public class {className}\n{{\n";
            foreach (var p in properties) {
                ret += $"  public {p.Type} {p.Prop};\n";
            }
            ret += "}";
            return ret;
        }
    }
    public class CodeBuilderExercise : IRunner
    {
        public void Run() {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            WriteLine(cb);
        }
    }
}
