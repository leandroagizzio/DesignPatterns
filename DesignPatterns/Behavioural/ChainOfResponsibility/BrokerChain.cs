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
            return base.ToString();
        }
    }
}
