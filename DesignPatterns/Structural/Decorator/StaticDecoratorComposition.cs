using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Decorator
{
    public abstract class ShapeFormat
    {
        public abstract string AsString();
    }

    public class Triangle : ShapeFormat
    {
        private float _size;

        public float Size { 
            get => _size; 
            set => _size = value; 
        }

        public Triangle() : this(0.0f) { }

        public Triangle(float size)
        {
            _size = size;
        }

        public override string AsString() => $"A triangle with size of {_size}";

    }

    public class ColoredShapee : ShapeFormat
    {
        private readonly ShapeFormat _shape;
        private readonly string _color;

        public ColoredShapee(ShapeFormat shape, string color)
        {
            _shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            _color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public override string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class ColoredShapeFormat<T> : ShapeFormat where T : ShapeFormat, new()
    {
        private string _color;
        private T shape = new T();

        public ColoredShapeFormat() : this("black") { }

        public ColoredShapeFormat(string color)
        {
            _color = color;
        }

        public override string AsString() => $"{shape.AsString()} has the color {_color}";
    }

    public class TransparentShapeFormat<T> : ShapeFormat where T : ShapeFormat, new()
    {
        private float _transparency;
        private T shape = new T();

        public TransparentShapeFormat() : this(0) { }

        public TransparentShapeFormat(float transparency)
        {
            _transparency = transparency;
        }

        public override string AsString() => $"{shape.AsString()} has the transparency of {_transparency}";
    }

    public class TransparentShapee : ShapeFormat
    {
        private readonly ShapeFormat _shape;
        private readonly float _transparency;

        public TransparentShapee(ShapeFormat shape, float transparency)
        {
            _shape = shape;
            _transparency = transparency;
        }

        public override string AsString() => $"{_shape.AsString()} has {_transparency * 100.0}% transparency";
    }
    
    public class StaticDecoratorComposition : IRunner
    {
        public void Run()
        {
            // TransparentShapee<ColoredShapee<Triangle>> shape 
            var redTriangle = new ColoredShapeFormat<Triangle>("red");
            WriteLine(redTriangle.AsString());
            var triangle = new TransparentShapeFormat<ColoredShapeFormat<Triangle>> (1);
            //var triangle = new TransparentShapeFormat<Triangle> (1);
            WriteLine(triangle.AsString());
        }
    }
}
