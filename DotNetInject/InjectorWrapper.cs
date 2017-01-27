using System.Diagnostics;
using System.Linq;
using SampleBundleApi.Injector;
using SampleBundleApi.Misc;

namespace DotNetInject
{
    public class InjectorWrapper : IInjectorWrapper
    {
        public bool Inject(string processName, string assemblyLocation, string className, string methodName)
        {
            var process = GetProcessByProcessName(processName);

            if (process == null) return false;

            ManagedInjector.Injector.Launch(process.MainWindowHandle, assemblyLocation, className, methodName);

            var injected = GetInjected(process);

            return injected;
        }

        private static bool GetInjected(Process process)
        {
            if (process == null) return false;

            var processModules = process.Modules.Cast<ProcessModule>();

            var injected = processModules.Any(module => module.FileName.Contains("ManagedInjector"));

            return injected;
        }

        private Process GetProcessByProcessName(string processName)
        {
            var processes = Process.GetProcesses();

            return processes.FirstOrDefault(pList => pList.ProcessName.StringCompare(processName));
        }
    }
}
