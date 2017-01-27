using System.Linq.Expressions;

namespace StrategyResolver.DynamicLinq
{
    internal class DynamicOrdering
    {
        public bool Ascending;
        public Expression Selector;
    }
}