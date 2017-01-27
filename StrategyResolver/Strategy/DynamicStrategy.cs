using System;
using System.Linq.Expressions;
using StrategyResolver.Strategy.Models;
using DynamicExpression = StrategyResolver.DynamicLinq.DynamicExpression;

namespace StrategyResolver.Strategy
{
    public class DynamicStrategy<T, T1> : IDynamicStrategy<T, T1> where T : IRuleValidateInfo where T1 : IRule
    {
        public virtual bool ValidateStrategy(T info)
        {
            return Validate(info);
        }

        public virtual void Initialise<T2>(T1 rule)
        {
            if (!string.IsNullOrEmpty(rule.ValidationLinq))
            {
                this.DynamicValidation = DynamicExpression.ParseLambda<T, bool>(rule.ValidationLinq);
            }

            this.Message = rule.Message;
        }

        public Expression<Func<T, bool>> DynamicValidation { get; set; }

        public bool Validate<T2>(T2 info) where T2 : T
        {
            return this.DynamicValidation != null ? this.DynamicValidation.Compile()(info) : ValidateStrategy(info);
        }

        public string Message { get; set; }
    }
}