using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.SOLID
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text) {
            entries.Add($"{++count}: {text}");
            return count; // memento pattern
        }

        public void RemoveEntry(int index) {
            entries.RemoveAt(index); // not stable as one remove messes the other indexes
        }

        public override string ToString() {
            return string.Join(Environment.NewLine, entries);
        }

        /*
        public void Save (string filename) {
            File.WriteAllText(filename, ToString());
        }

        public static void Load (string filename) {

        }

        public void Load(Uri uri) {

        }*/

    }

    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false) {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }
    }
    public class SingleResponsibility : IRunner
    {
        public void Run() {
            var j = new Journal();
            j.AddEntry("I laughed today");
            j.AddEntry("I ate a peach");
            WriteLine(j);

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";

            p.SaveToFile(j, filename, true);
            Process.Start(filename);

        }
    }
}
