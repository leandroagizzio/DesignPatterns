using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Decorator
{
    public interface IShape
    {
        string AsString();
    }

    public abstract class Shape
    {
        public virtual string AsString() => string.Empty;
    }

    public class Circle : IShape
    {
        private float _radius;

        public Circle(float radius)
        {
            _radius = radius;
        }

        public string AsString() => $"A circle with radius of {_radius}";

        public void Resize(float factor)
        {
            _radius *= factor;
        }
    }
    public class Circle2 : Shape
    {
        private float _radius;

        public Circle2(float radius)
        {
            _radius = radius;
        }

        public override string AsString() => $"A circle with radius of {_radius}";

        public void Resize(float factor)
        {
            _radius *= factor;
        }
    }

    public class Square : IShape
    {
        private float _side;

        public Square(float side)
        {
            _side = side;
        }

        public string AsString() => $"A square with side of {_side}";
    }

    // 2
    // Foo, Foo<T> : Foo

    public abstract class ShapeDecorator : Shape
    {
        protected internal readonly List<Type> types = new();
        protected internal Shape shape;

        public ShapeDecorator(Shape shape)
        {
            this.shape = shape;
            if (shape is ShapeDecorator sd)
                types.AddRange(sd.types);
        }
    }

    public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
    {
        protected readonly TCyclePolicy policy = new();

        protected ShapeDecorator(Shape shape) : base(shape)
        {
            if (policy.TypeAdditionAllowed(typeof(TSelf), types))
                types.Add(typeof(TSelf));
        }
    }

    //public class ShapeDecoratorWithPolicy<T> : ShapeDecorator<T, ThrowOnCyclePolicy>
    public class ShapeDecoratorWithPolicy<T> : ShapeDecorator<T, CyclesAllowedPolicy>
    {
        public ShapeDecoratorWithPolicy(Shape shape) : base(shape)
        {
        }
    }

    public class ColoredShape2
        // : ShapeDecorator<ColoredShape, ThrowOnCyclePolicy>
        : ShapeDecorator<ColoredShape2, AbsorbCyclePolicy>
        // : ShapeDecoratorWithPolicy<ColoredShape>
    {
        private readonly Shape _shape;
        private readonly string _color;

        public ColoredShape2(Shape shape, string color) : base(shape) 
        {
            _shape = shape;
            _color = color;
        }

        //public override string AsString() => $"{_shape.AsString()} has the color {_color}";
        public override string AsString()
        {
            var sb = new StringBuilder($"{_shape.AsString()}");
            if (policy.ApplicationAllowed(types[0], types.Skip(1).ToList()))
                sb.Append($" has the color {_color}");

            return sb.ToString();
        }
    }

    public class ColoredShape : IShape
    {
        private readonly IShape _shape;
        private readonly string _color;

        public ColoredShape(IShape shape, string color)
        {
            _shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            _color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class TransparentShape : IShape
    {
        private readonly IShape _shape;
        private readonly float _transparency;

        public TransparentShape(IShape shape, float transparency)
        {
            _shape = shape;
            _transparency = transparency;
        }

        public string AsString() => $"{_shape.AsString()} has {_transparency * 100.0}% transparency";
    }

    public abstract class ShapeDecoratorCyclePolicy
    {
        public abstract bool TypeAdditionAllowed(Type type, IList<Type> allTypes);
        public abstract bool ApplicationAllowed(Type type, IList<Type> allTypes);
    }

    public class CyclesAllowedPolicy : ShapeDecoratorCyclePolicy
    {
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }

        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
    }

    public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
    {
        private bool handler(Type type, IList<Type> allTypes)
        {
            if (allTypes.Contains(type))
                throw new InvalidOperationException($"Cycle detected! Type is already a {type.FullName}");
            return true;
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return handler(type, allTypes);
        }

        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return handler(type, allTypes);
        }
    }

    public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
    {
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return !allTypes.Contains(type);
            
        }

        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
    }

    public class DynamicDecoratorComposition : IRunner
    {
        public void Run()
        {
            var square = new Square(1.23f);
            WriteLine(square.AsString());

            var redSquare = new ColoredShape(square, "red");
            WriteLine(redSquare.AsString());

            var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
            WriteLine(redHalfTransparentSquare.AsString());

            var circle = new Circle2(2);
            var colored1 = new ColoredShape2(circle, "red");
            var colored2 = new ColoredShape2(colored1, "blue");
            
            WriteLine(colored2.AsString());
        }
    }
}
