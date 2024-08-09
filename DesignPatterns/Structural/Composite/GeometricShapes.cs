﻿using System;
using System.Text;
using static System.Console;

namespace DesignPatterns.Structural.Composite
{
    public class GraphicObject
    {
        public virtual string Name { get; set; } = "Group";
        public string Color;

        private Lazy<List<GraphicObject>> _children = new();
        public List<GraphicObject> Children => _children.Value;

        private void Print(StringBuilder sb, int depth)
        {
            sb.Append(new string('*', depth))
              .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
              .AppendLine(Name);
            foreach (var child in Children)
            {
                child.Print(sb, depth+1);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            Print(sb, 0);
            return sb.ToString();
        }
    }
    public class Circle : GraphicObject
    {
        public override string Name => "Circle";
    }
    public class Square : GraphicObject
    {
        public override string Name => "Square";
    }
    public class GeometricShapes : IRunner
    {
        public void Run()
        {
            var drawing = new GraphicObject { Name = "My Drawing" };
            drawing.Children.Add(new Square { Color = "Red" });
            drawing.Children.Add(new Circle { Color = "Yellow" });

            var group = new GraphicObject();
            group.Children.Add(new Circle { Color = "Blue"});
            group.Children.Add(new Square { Color = "Blue"});

            drawing.Children.Add(group);

            WriteLine(drawing);
        }
    }
}
