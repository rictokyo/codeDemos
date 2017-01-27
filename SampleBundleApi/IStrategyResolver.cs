namespace SampleBundleApi
{
    public interface IStrategyResolver
    {
        void Resolve(IConditionalState state);
    }
}