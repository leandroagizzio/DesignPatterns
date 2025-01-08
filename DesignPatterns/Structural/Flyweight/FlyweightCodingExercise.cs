using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Flyweight
{
    public class Sentence
    {
        private string[] _sentence;
        private WordToken[] _words;
        public Sentence(string plainText)
        {
            // todo
            _sentence = plainText.Split(' ');
            //_words = new WordToken[_sentence.Length];
            _words = plainText.Split(' ').Select(_  => new WordToken()).ToArray();
        }

        public WordToken this[int index]
        {
            get
            {
                // todo
                return _words[index];
            }
            set
            {
                _words[index] = value;
            }
        }

        public override string ToString()
        {
            // output formatted text here
            StringBuilder sb = new StringBuilder();
            for (int i=0; i < _sentence.Length; i++)
            {
                if (_words[i].Capitalize)
                    sb.Append(_sentence[i].ToUpper());
                else
                    sb.Append(_sentence[i]);
                sb.Append(' ');
            }
            return sb.ToString().Trim();
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }

    internal class FlyweightCodingExercise : IRunner
    {
        public void Run()
        {
            var sentence = new Sentence("hello word");
            sentence[1].Capitalize = true;
            WriteLine(sentence);
        }
    }
}
