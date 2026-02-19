using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioural.ChainOfResponsibility
{
    public abstract class Creaturee
    {
        public int Attack { get; set; }
        public int Defense { get; set; }

    }

    public class Goblin : Creaturee
    {
        public Goblin(Gamee game) { }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Gamee game) : base (game) { }
    }

    public class Gamee
    {
        public IList<Creaturee> Creatures;
    }

    public class ChainOfResponsibilityCodingExercise : IRunner
    {
        public void Run()
        {
            
        }
    }
}
