using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public Rectangle() {

        }
        public Rectangle(int width, int height) {
            Width = width;
            Height = height;
        }
        public override string ToString() {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width { // old -> public new int Width
            set { base.Width = base.Height = value; }
        }
        public override int Height {
            set { base.Width = base.Height = value; }
        }
    }

    public class LiskovSubstitution : IRunner
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        
        public void Run() {
            Rectangle rc = new Rectangle(2, 3);
            WriteLine($"{rc} has area of {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;
            WriteLine($"{sq} has area of {Area(sq)}");
        }
    }
}
