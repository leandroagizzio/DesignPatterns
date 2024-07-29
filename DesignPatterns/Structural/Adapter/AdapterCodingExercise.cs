using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.Adapter
{
    public class Square
    {
        public int Side;
    }
    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }
    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }
    public class SquareToRectangleAdapter : IRectangle
    {
        private int _side;
        public SquareToRectangleAdapter(Square square)
        {
            // todo
            _side = square.Side;
        }
        // todo
        public int Width => _side;

        public int Height => _side;
        
    }
    public class AdapterCodingExercise : IRunner
    {
        public void Run()
        {
            
        }
    }
}
