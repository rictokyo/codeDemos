using System.Collections.Generic;
using StrategyResolver.Strategy.Models;

namespace StrategyResolver.Strategy
{
    public interface IStrategyTypeResolver<out T, T1, T2, in T3>
        where T : IStrategy<T1, T2>
        where T1 : IRule
        where T2 : IRuleValidateInfo
        where T3 : IRuleContainer<T1>
    {
        IEnumerable<T> GetResolvedValidators(T3 dsr) ;
    }
}