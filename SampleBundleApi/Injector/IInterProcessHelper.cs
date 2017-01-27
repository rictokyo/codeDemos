using System.Diagnostics;

namespace SampleBundleApi.Injector
{
    public interface IInterProcessHelper
    {
        void InjectService<T>(string processName);
        void InjectService<T>(Process sampleProcess);
    }
}