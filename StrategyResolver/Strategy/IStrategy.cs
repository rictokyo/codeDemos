namespace StrategyResolver.Strategy
{
    public interface IStrategy<in T1, in T2> 
        where T2 : IRuleValidateInfo
    {
        bool ValidateStrategy(T2 info);
        void Initialise<T>(T1 rule);
    }  
}