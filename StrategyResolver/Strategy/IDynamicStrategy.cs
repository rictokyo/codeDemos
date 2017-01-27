using System;
using System.Linq.Expressions;
using StrategyResolver.Strategy.Models;

namespace StrategyResolver.Strategy
{
    public interface IDynamicStrategy<T, in T1> : IStrategy<T1, T> where T1 : IRule where T : IRuleValidateInfo
    {
        Expression<Func<T, bool>> DynamicValidation { get; set; }
        bool Validate<T2>(T2 info) where T2 : T;
        string Message { get; }
    }
}