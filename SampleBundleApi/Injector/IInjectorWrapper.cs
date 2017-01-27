namespace SampleBundleApi.Injector
{
    public interface IInjectorWrapper
    {
        bool Inject(string processName, string assemblyLocation, string className, string methodName);
    }
}