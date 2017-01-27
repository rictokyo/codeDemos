using StrategyResolver.Strategy.Models;

namespace StrategyResolver.Strategy
{
    public class SimpleDynamicStrategy<T> : DynamicStrategy<T, Rule> where T : IRuleValidateInfo
    {       
    }
}