using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Windows;
using SampleBundleApi.Wcf;

namespace DotNetInjectable
{
    public class ServiceLoader
    {
        public static void Start()
        {
            try
            {
                var currentDomain = AppDomain.CurrentDomain;

                currentDomain.AssemblyResolve += LoadFromSameFolder;

                var theType = Type.GetType("DotNetInjectable.ServiceLoader+SampleService, DotNetInjectable");

                if (theType == null) return;

                dynamic insightService = Activator.CreateInstance(theType);

                insightService.Start("net.pipe://localhost/B/", "PipeService");
            }
            catch (Exception exc)
            {
                Trace.WriteLine(exc.Message);
            }
        }

        protected static Assembly LoadFromSameFolder(object sender, ResolveEventArgs args)
        {
            Assembly assembly = null;

            try
            {
                var folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (folderPath == null) return null;

                var assemblyName = string.Format("{0}.dll", new AssemblyName(args.Name).Name);

                var assemblyPath = Path.Combine(folderPath, assemblyName);

                if (File.Exists(assemblyPath) == false) return null;

                assembly = Assembly.LoadFrom(assemblyPath);
            }
            catch (Exception exc)
            {
                Trace.WriteLine(exc.Message);
            }

            return assembly;
        }

        [ServiceBehavior(IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
        public class SampleService : WcfService<SampleService, IHelloWorldService>, IHelloWorldService
        {
            public void Say(string message)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Application.Current.MainWindow.Title = message;                   
                });

            }
        }
    }
}