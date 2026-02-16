using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Proxy
{
    public class CompositeProxySoAAoS : IRunner
    {
        public void Run() {
            var creatures = new Creature[100];
            foreach (var c in creatures) {
                c.X++;
            }

            var creatures2 = new Creatures(100);
            foreach (var c in creatures2) {
                c.X++;
            }
        }

        public class Creature 
        {
            public byte Age;
            public int X, Y;
        }

        public class Creatures 
        {
            private readonly int _size;
            private byte[] _age;
            private int[] _x, _y;

            public Creatures(int size) {
                _size = size;
                _age = new byte[size];
                _x = new int[size];
                _y = new int[size];
            }

            public struct CreatureProxy 
            {
                private readonly Creatures _creatures;
                private readonly int _index;

                public CreatureProxy(Creatures creatures, int index) {
                    _creatures = creatures;
                    _index = index;
                }

                public ref byte Age => ref _creatures._age[_index];
                public ref int X => ref _creatures._x[_index];
                public ref int Y => ref _creatures._y[_index];
            }

            public IEnumerator<CreatureProxy> GetEnumerator() {
                for (int pos = 0; pos < _size; pos++)
                    yield return new CreatureProxy(this, pos);
            }

        }
    }

}
