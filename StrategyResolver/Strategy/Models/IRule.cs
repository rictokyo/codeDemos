namespace StrategyResolver.Strategy.Models
{
    public interface IRule
    {
        string Name { get; set; }
        int ExecutionOrder { get; set; }
        string ValidationLinq { get; set; }
        string Message { get; set; }
    }
}