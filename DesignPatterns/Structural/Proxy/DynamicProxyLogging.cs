using ImpromptuInterface;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Proxy
{
    public class DynamicProxyLogging : IRunner
    {
        public void Run() {
            //var ba = new BankAccount();
            var ba = Log<BankAccount>.As<IBankAccount>();
            
            ba.Deposit(100);
            ba.Deposit(200);
            ba.Deposit(250);
            ba.Withdraw(50);
            ba.Withdraw(25);

            WriteLine(ba);
        }

        public interface IBankAccount
        {
            void Deposit(int amount);
            bool Withdraw(int amount);
            string ToString();
        }

        public class BankAccount : IBankAccount
        {
            private int balance;
            private int overdraftLimit = -500;

            public void Deposit(int amount) {
                balance += amount;
                WriteLine($"Deposited ${amount}, balance is now {balance}");
            }

            public bool Withdraw(int amount) {
                if (balance - amount >= overdraftLimit) {
                    balance -= amount;
                    WriteLine($"Withdrew ${amount}, balance is now {balance}");
                    return true;
                }
                return false;
            }

            public override string ToString() {
                return $"{nameof(balance)}: {balance}";
            }
        }

        public class Log<T> : DynamicObject where T : class, new() 
        {
            private readonly T _subject;
            private Dictionary<string, int> _methodCallCount = new();

            public Log(T subject) {
                _subject = subject;
            }

            public static I As<I>() where I : class {
                if (!typeof(I).IsInterface)
                    throw new ArgumentException("I must be an interface type!");

                return new Log<T>(new T()).ActLike<I>();
            }

            public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result) {
                try {
                    WriteLine($"Invoking {_subject.GetType().Name}.{binder.Name} with arguments [{(args?.Length > 0 ? string.Join(", ", args!) : string.Empty)}]");
                    if (_methodCallCount.ContainsKey(binder.Name)) _methodCallCount[binder.Name]++;
                    else _methodCallCount.Add(binder.Name, 1);
                    result = _subject?.GetType()?.GetMethod(binder.Name)?.Invoke(_subject, args);
                    return true;
                } catch {
                    result = null;
                    return false;
                }                
            }

            public string Info {
                get {
                    var sb = new StringBuilder();
                    foreach (var kv in _methodCallCount)
                        sb.AppendLine($"{kv.Key} called {kv.Value} time(s)");
                    return sb.ToString();
                }
            }

            public override string ToString() {
                return $"{Info}{_subject}";
            }

        }
    }
}
