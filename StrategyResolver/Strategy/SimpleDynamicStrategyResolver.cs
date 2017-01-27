using StrategyResolver.Strategy.Models;

namespace StrategyResolver.Strategy
{
    public class SimpleDynamicStrategyResolver<T> : StrategyTypeResolver<SimpleDynamicStrategy<T>, Rule, T, Rules> where T : IRuleValidateInfo
    {
        
    }
}