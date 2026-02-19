using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Behavioural.ChainOfResponsibility
{
    public class BrokerChain : IRunner
    {
        public void Run() {
            var game = new Game();
            var monster = new Monster(game, "Goblin", 3, 4);
            WriteLine(monster);

            using (new TripleAttackModifier(game, monster)) {
                WriteLine(monster);
                using (new EnhanceDefenseModifier(game, monster)) {
                    WriteLine(monster);
                }
            }

            WriteLine(monster);
        }
    }

    public class Game
    {
        public event EventHandler<Query> Queries;

        public void PerformQuery(object sender, Query q) {
            Queries?.Invoke(sender, q);
        }
    }

    public class Query
    {
        public string MonsterName;

        public enum Argument
        {
            Attack, Defense
        }

        public Argument WhatToQuery;

        public int Value;

        public Query(string monsterName, Argument whatToQuery, int value) {
            MonsterName = monsterName ?? throw new ArgumentNullException(nameof(monsterName));
            WhatToQuery = whatToQuery;
            Value = value;
        }

    }

    public class Monster
    {
        private Game _game;
        public string Name;
        private int _attack, _defense;

        public Monster(Game game, string name, int attack, int defense) {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _attack = attack;
            _defense = defense;
        }

        public int Attack {
            get {
                var q = new Query(Name, Query.Argument.Attack, _attack);
                _game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public int Defense {
            get {
                var q = new Query(Name, Query.Argument.Defense, _defense);
                _game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public override string ToString() {
            return this.OverrideToString();
        }
    }

    public abstract class MonsterModifier : IDisposable
    {
        protected Game _game;
        protected Monster _monster;

        protected MonsterModifier(Game game, Monster monster) {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _monster = monster ?? throw new ArgumentNullException(nameof(monster));
            _game.Queries += Handle;
        }

        protected abstract void Handle(object sender, Query q);

        public void Dispose() {
            _game.Queries -= Handle;
        }
    }

    public class TripleAttackModifier : MonsterModifier
    {
        public TripleAttackModifier(Game game, Monster monster) : base(game, monster) {
        }

        protected override void Handle(object sender, Query q) {
            if (q.MonsterName == _monster.Name && q.WhatToQuery == Query.Argument.Attack)
                q.Value *= 3;
        }
    }

    public class EnhanceDefenseModifier : MonsterModifier
    {
        public EnhanceDefenseModifier(Game game, Monster monster) : base(game, monster) {
        }

        protected override void Handle(object sender, Query q) {
            if (q.MonsterName == _monster.Name && q.WhatToQuery == Query.Argument.Defense)
                q.Value += 2;
        }
    }
}
