using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioural.ChainOfResponsibility
{
    public abstract class Creature
    {
        public int Attack { get; set; }
        public int Defense { get; set; }

    }

    public class Goblin : Creature
    {
        public Goblin(Game game) { }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base (game) { }
    }

    public class Game
    {
        public IList<Creature> Creatures;
    }

    public class ChainOfResponsibilityCodingExercise : IRunner
    {
        public void Run()
        {
            
        }
    }
}
