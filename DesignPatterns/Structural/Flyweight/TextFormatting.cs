using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Flyweight
{
    public class FormattedText
    {
        private readonly string _plainText;
        private bool[] capitalize;

        public FormattedText(string plainText)
        {
            _plainText = plainText;
            capitalize = new bool[plainText.Length];
        }
        public void Capitalize(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                capitalize[i] = true;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            int i = 0;
            foreach (var b in capitalize)
            {
                var e = _plainText.ElementAt(i++);
                sb.Append(b ? char.ToUpper(e) : e);
            }
            return sb.ToString();
        }
    }

    public class BetterFormattedText
    {
        private readonly string _plainText;
        private List<TextRange> _formatting = new();

        public BetterFormattedText(string plainText)
        {
            _plainText = plainText;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _plainText.Length; i++)
            {
                var c = _plainText[i];
                foreach (var range in _formatting)
                    if (range.Covers(i) && range.Capitalize)
                        c = char.ToUpper(c);
                sb.Append(c);
            }
            return sb.ToString();
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange { Start = start, End = end };
            _formatting.Add(range);
            return range;
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;

            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }
    }

    public class TextFormatting : IRunner
    {
        public void Run()
        {
            var ft = new FormattedText("This is a brave new world");
            ft.Capitalize(10, 15);
            WriteLine(ft);


            var bft = new BetterFormattedText("This is a brave new world");
            bft.GetRange(10, 15).Capitalize = true;
            WriteLine(bft);
        }
    }
}
