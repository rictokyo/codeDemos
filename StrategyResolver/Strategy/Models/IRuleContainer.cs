using System.Collections.Generic;

namespace StrategyResolver.Strategy.Models
{
    public interface IRuleContainer<out T> where T : IRule
    {
        IEnumerable<T> GetRules();
        string Namespace { get; }
        string AssemblyName { get; }
    }
}