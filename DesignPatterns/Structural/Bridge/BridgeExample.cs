using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Bridge
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
    }
    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            WriteLine($"Drawing a circle of radius {radius}");
        }
    }
    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            WriteLine($"Drawing pixels for circle with radius {radius}");
        }
    }
    public abstract class Shape
    {
        protected IRenderer renderer;

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }
        public abstract void Draw();
        public abstract void Resize(float factor);
    }
    public class Circle : Shape
    {
        private float _radius;
        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            _radius = radius;
        }

        public override void Draw()
        {
            renderer.RenderCircle( _radius );
        }

        public override void Resize(float factor)
        {
            _radius *= factor;
        }
    }

    public class BridgeExample : IRunner
    {
        public void Run()
        {
            ////IRenderer renderer = new VectorRenderer();
            //IRenderer renderer = new RasterRenderer();
            //var circle = new Circle(renderer, 5);

            //circle.Draw();
            //circle.Resize(2);
            //circle.Draw();

            var cb = new ContainerBuilder();
            cb.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();
            cb.Register((c, p) =>
                new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0) )
            );

            using var c = cb.Build();

            var circle = c.Resolve<Circle>(
                new PositionalParameter(0, 5.0f)
            );

            circle.Draw();
            circle.Resize(3);
            circle.Draw();
        }
    }
}
