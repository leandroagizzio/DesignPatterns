using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.Composite
{

    public abstract class ISpecification<T>
    {
        public abstract bool IsSatisfied(T t);

        public static ISpecification<T> operator &(ISpecification<T> first, ISpecification<T> second)
        {
            return new AndSpecification<T>(first, second);
        }
    }

    public abstract class CompositeSpecification<T> : ISpecification<T> 
    {
        protected readonly ISpecification<T>[] _items;

        public CompositeSpecification(params ISpecification<T>[] items)
        {
            _items = items;
        }

    }

    // combinator
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(params ISpecification<T>[] items) : base(items)
        {
        }

        public override bool IsSatisfied(T t)
        {
            return _items.All(i => i.IsSatisfied(t));
        }
    }

    public class CompositeSpecification : IRunner
    {
        public void Run()
        {
            
        }
    }
}
