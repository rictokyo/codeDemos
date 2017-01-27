using System.Collections.Generic;
using System.Linq;
using StrategyResolver.Strategy.Models;

namespace StrategyResolver.Strategy
{
    public class StrategyTypeResolver<T, T1, T2, T3> : IStrategyTypeResolver<T, T1, T2, T3>
        where T : IStrategy<T1, T2>
        where T1 : IRule 
        where T2 : IRuleValidateInfo
        where T3 : IRuleContainer<T1>
    {
        /// <summary>
        /// Resolves a list of strategies from config
        /// </summary>
        /// <param name="dsr">The rules container</param>
        /// <returns>An enumerable of the strategies</returns>
        public IEnumerable<T> GetResolvedValidators(T3 dsr)
        {           
            foreach (var rule in dsr.GetRules().OrderBy(x => x.ExecutionOrder))
            {                
                var typeName = string.Format("{0}.{1}", dsr.Namespace, rule.Name);

                var strategy = ReflectionHelper.GetTypeFromString<T>(dsr.AssemblyName, typeName);

                strategy.Initialise<T2>(rule);

                yield return strategy;
            }
        }

        /// <summary>
        /// Resolves a list of strategies from config
        /// </summary>
        /// <param name="dsr">The rules container</param>
        /// <returns>An enumerable of the strategies</returns>
        public IEnumerable<T> GetResolvedValidatorsGeneric(T3 dsr)
        {
            foreach (var rule in dsr.GetRules().OrderBy(x => x.ExecutionOrder))
            {
                var typeName = string.Format("{0}.{1}", dsr.Namespace, rule.Name);

                var strategy = ReflectionHelper.GetGenericTypeFromString<T, T2>(dsr.AssemblyName, typeName);

                strategy.Initialise<T2>(rule);

                yield return strategy;
            }
        }
    }
}