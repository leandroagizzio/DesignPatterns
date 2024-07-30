using System;
using static System.Console;

namespace DesignPatterns.Structural.Bridge
{

    public class BridgeCodingExercise : IRunner
    {
        public abstract class Shape
        {
            protected IRenderer Renderer;

            protected Shape(IRenderer renderer)
            {
                Renderer = renderer;
            }

            public string Name { get; set; }
            public override string ToString() => $"Drawing {Name} as {Renderer.WhatToRenderAs}";
        }
        public class VectorRenderer : IRenderer
        {
            public string WhatToRenderAs => "lines";
        }
        public class RasterRenderer : IRenderer
        {
            public string WhatToRenderAs => "pixels";
        }

        public class Triangle : Shape
        {
            public Triangle(IRenderer renderer) : base (renderer) {
                Name = "Triangle";
            } 
        }

        public class Square : Shape
        {
            public Square(IRenderer renderer) : base(renderer)
            {
                Name = "Square";
            }
        }

        //public class VectorSquare : Square
        //{
        //    public override string ToString() => "Drawing {Name} as lines";
        //}

        //public class RasterSquare : Square
        //{
        //    public override string ToString() => "Drawing {Name} as pixels";
        //}

        // imagine VectorTriangle and RasterTriangle are here too

        public interface IRenderer
        {
            string WhatToRenderAs { get; }
        }

        public void Run()
        {
            WriteLine(new Triangle(new RasterRenderer()).ToString());
            WriteLine(new Square(new VectorRenderer()).ToString());
        }
    }
}
