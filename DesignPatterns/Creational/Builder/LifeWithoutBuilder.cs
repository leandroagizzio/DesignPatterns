﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Creational.Builder
{
    public class LifeWithoutBuilder : IRunner
    {
        public class HtmlElement
        {
            public string Name, Text;
            public List<HtmlElement> Elements = new List<HtmlElement>();
            private const int indentSize = 2;

            public HtmlElement() {

            }

            public HtmlElement(string name, string text) {
                Name = name;
                Text = text;
            }

            private string ToStringImpl(int indent) {
                var sb = new StringBuilder();
                var i = new string(' ', indentSize * indent);
                sb.AppendLine($"{i}<{Name}>");

                if (!string.IsNullOrWhiteSpace(Text)) {
                    sb.Append(new string(' ', indentSize * (indent+1)));
                    sb.AppendLine(Text);
                }

                foreach (var e in Elements) {
                    sb.Append(e.ToStringImpl(indent + 1));
                }

                sb.AppendLine($"{i}</{Name}>");

                return sb.ToString();
            }

            public override string ToString() {
                return ToStringImpl(0);
            }
        }

        public class HtmlBuilder
        {
            private readonly string _rootName;
            HtmlElement root = new HtmlElement();
            public HtmlBuilder(string rootName) {
                root.Name = rootName;
                _rootName = rootName;
            }

            public HtmlBuilder AddChild(string childName, string childText) {
                var e = new HtmlElement(childName, childText);
                root.Elements.Add(e);
                //fluent builder
                return this;
            }

            public override string ToString() {
                return root.ToString();
            }

            public void Clear() {
                root = new HtmlElement { Name = _rootName };
            }
        }

        public void Run() {
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            WriteLine(sb);

            var words = new[] { "hello", "world"};
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words) {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");
            WriteLine(sb);

            var builder = new HtmlBuilder("ul");
            //builder.AddChild("li", "hello");
            //builder.AddChild("li", "world");
            
            //fluent builder
            builder.AddChild("li", "hello").AddChild("li", "world");
            
            WriteLine(builder.ToString());
            

        }
    }
}
