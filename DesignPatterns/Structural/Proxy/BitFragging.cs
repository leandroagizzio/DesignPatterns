using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DesignPatterns.Structural.Proxy.BitFragging;
using static System.Console;

namespace DesignPatterns.Structural.Proxy
{
    public class BitFragging : IRunner
    {
        public void Run() {
            // 1-3-5+7
            // 0 1 2 ... 10

            var numbers = new[] { 1, 3, 5, 7 };
            var numberOfOps = numbers.Length - 1;

            for (int result=0; result <= 10; result++) {
                for (var key = 0UL; key < (1UL << 2*numberOfOps); key++) {
                    var tbs = new TwoBitSet(key);
                    var ops = Enumerable.Range(0, numberOfOps).Select(i => tbs[i]).Cast<Op>().ToArray();
                    var problem = new Problem(numbers, ops);
                    if (problem.EVal() == result) {
                        WriteLine($"{new Problem(numbers, ops)} = {result}");
                        break;
                    }
                }
            }
        }

        public enum Op : byte 
        {
            [Description("*")]
            Mul = 0,
            [Description("/")]
            Div = 1,
            [Description("+")]
            Add = 2,
            [Description("-")]
            Sub = 3
        }
        public class TwoBitSet 
        { // 64 bits -> 32 values
            private readonly ulong _data;

            public TwoBitSet(ulong data) {
                _data = data;
            }

            // 00 10 01 01
            public byte this[int index] {
                get {
                    var shift = index << 1;
                    ulong mask = (0b11U << shift); // 00 10 01 01
                    return (byte)((_data & mask) >> shift);
                }
            }
        }

        public class Problem 
        {
            // 1 3 5 7
            // Op.Add Op.Mul Op.Add
            private readonly List<int> _numbers;
            private readonly List<Op> _ops;

            public Problem(IEnumerable<int> numbers, IEnumerable<Op> ops) {
                _numbers = new List<int>(numbers);
                _ops = new List<Op>(ops);
            }

            public int EVal() {
                var opGroups = new[] {
                    new [] { Op.Mul, Op.Div },
                    new [] { Op.Add, Op.Sub }
                };
                startAgain:
                foreach (var group in opGroups) {
                    for (int idx=0; idx < _ops.Count; idx++) {
                        if (group.Contains(_ops[idx])) {
                            var op = _ops[idx];
                            var result = op.Call(_numbers[idx], _numbers[idx + 1]);

                            if (result != (int)result) 
                                return int.MinValue;

                            _numbers[idx] = (int)result;
                            _numbers.RemoveAt(idx+1);
                            _ops.RemoveAt(idx);

                            if (_numbers.Count == 1)
                                return _numbers[0];
                            goto startAgain;
                        }
                    }
                }

                return _numbers[0];
            }

            public override string ToString() {
                var sb = new StringBuilder();
                int i = 0;

                for (; i < _ops.Count; ++i) {
                    sb.Append(_numbers[i]);
                    sb.Append(_ops[i].Name());
                }

                sb.Append(_numbers[i]);
                return sb.ToString();
            }

        }
    }

    // Op -> name
    public static class OpImpl
    {
        static OpImpl() {
            var type = typeof(Op);
            foreach (Op op in Enum.GetValues(type)) {
                MemberInfo[] memInfo = type.GetMember(op.ToString());
                if (memInfo.Length > 0) {
                    var attrs = memInfo[0].GetCustomAttributes(
                      typeof(DescriptionAttribute), false);

                    if (attrs.Length > 0) {
                        opNames[op] = ((DescriptionAttribute)attrs[0]).Description[0];
                    }
                }
            }
        }

        private static readonly Dictionary<Op, char> opNames
          = new Dictionary<Op, char>();

        // notice the data types!
        private static readonly Dictionary<Op, Func<double, double, double>> opImpl =
          new Dictionary<Op, Func<double, double, double>>() {
              [Op.Mul] = ((x, y) => x * y),
              [Op.Div] = ((x, y) => x / y),
              [Op.Add] = ((x, y) => x + y),
              [Op.Sub] = ((x, y) => x - y),
          };

        public static double Call(this Op op, int x, int y) {
            return opImpl[op](x, y);
        }

        public static char Name(this Op op) {
            return opNames[op];
        }
    }

}
