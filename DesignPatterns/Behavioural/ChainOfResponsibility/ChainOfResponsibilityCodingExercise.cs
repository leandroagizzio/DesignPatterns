using DesignPatterns.Behavioural.ChainOfResponsibility;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Behavioural.ChainOfResponsibility
{
 
    public class ChainOfResponsibilityCodingExercise : IRunner
    {
        public void Run()
        {
            var game = new Jogo();
            var goblin = new Saci(game);
            game.Criaturas.Add(goblin);

            WriteLine("1? " + goblin.Attack);
            WriteLine("1? " + goblin.Defense);

            var goblin2 = new Saci(game);
            game.Criaturas.Add(goblin2);

            WriteLine("1? " + goblin.Attack);
            WriteLine("2? " + goblin.Defense);

            var goblin3 = new ReiSaci(game);
            game.Criaturas.Add(goblin3);

            WriteLine("2? " + goblin.Attack);
            WriteLine("3? " + goblin.Defense);
        }
    }

    public abstract class Criatura
    {
        protected Jogo game;
        protected readonly int baseAttack;
        protected readonly int baseDefense;

        protected Criatura(Jogo game, int baseAttack, int baseDefense) {
            this.game = game;
            this.baseAttack = baseAttack;
            this.baseDefense = baseDefense;
        }

        public virtual int Attack { get; set; }
        public virtual int Defense { get; set; }
        public abstract void Query(object source, StatQuery sq);
    }

    public class Saci : Criatura
    {
        public override void Query(object source, StatQuery sq) {
            if (ReferenceEquals(source, this)) {
                switch (sq.Statistic) {
                    case Statistic.Attack:
                        sq.Result += baseAttack;
                        break;
                    case Statistic.Defense:
                        sq.Result += baseDefense;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            } else {
                if (sq.Statistic == Statistic.Defense) {
                    sq.Result++;
                }
            }
        }

        public override int Defense {
            get {
                var q = new StatQuery { Statistic = Statistic.Defense };
                foreach (var c in game.Criaturas)
                    c.Query(this, q);
                return q.Result;
            }
        }

        public override int Attack {
            get {
                var q = new StatQuery { Statistic = Statistic.Attack };
                foreach (var c in game.Criaturas)
                    c.Query(this, q);
                return q.Result;
            }
        }

        public Saci(Jogo game) : base(game, 1, 1) {
        }

        protected Saci(Jogo game, int baseAttack, int baseDefense) : base(game,
          baseAttack, baseDefense) {
        }
    }

    public class ReiSaci : Saci
    {
        public ReiSaci(Jogo game) : base(game, 3, 3) {
        }

        public override void Query(object source, StatQuery sq) {
            if (!ReferenceEquals(source, this) && sq.Statistic == Statistic.Attack) {
                sq.Result++; // every goblin gets +1 attack
            } else base.Query(source, sq);
        }
    }

    public enum Statistic
    {
        Attack,
        Defense
    }

    public class StatQuery
    {
        public Statistic Statistic;
        public int Result;
    }

    public class Jogo
    {
        public IList<Criatura> Criaturas = new List<Criatura>();
    }



}
