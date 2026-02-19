using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Behavioural.ChainOfResponsibility
{
    public class MethodChain : IRunner
    {
        public void Run() {
            var goblin = new Creature("Goblin", 3, 2);
            WriteLine(goblin);

            var root = new CreatureModifier(goblin);

            root.Add(new NoBonusModifier(goblin));
            
            WriteLine("double");
            root.Add(new DoubleAttackModifier(goblin));
            
            WriteLine("defense");
            root.Add(new IncreaseDefenseModifier(goblin));

            root.Handle();
            WriteLine(goblin);


        }
    }

    public class Creature 
    {
        public string Name;
        public int Attack;
        public int Defense;

        public Creature(string Name, int Attack, int Defense) {
            this.Name = Name;
            this.Attack = Attack;
            this.Defense = Defense;
        }

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier 
    {
        protected Creature creature;
        protected CreatureModifier? next;

        public CreatureModifier(Creature creature) {
            this.creature = creature;
        }

        public void Add(CreatureModifier cm) {
            if (next != null) 
                next.Add(cm);
            else
                next = cm;
        }

        public virtual void Handle() => next?.Handle();
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature) {
        }

        public override void Handle() {            
            WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Creature creature) : base(creature) {
        }

        public override void Handle() {            
            WriteLine($"Increasing {creature.Name}'s defense");
            creature.Defense += 3;
            base.Handle();
        }
    }

    public class NoBonusModifier : CreatureModifier
    {
        public NoBonusModifier(Creature creature) : base(creature) {
        }

        public override void Handle() {
            WriteLine("No bonuses for you!");
        }

    }
}
