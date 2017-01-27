using System;
using System.IO;
using System.Reflection;

namespace SampleBundleApi.Wcf
{
    public abstract class WcfServiceBase : IWcfService
    {
        protected WcfServiceBase()
        {
            var currentDomain = AppDomain.CurrentDomain;

            currentDomain.AssemblyResolve += LoadFromSameFolder;
        }

        public virtual void Start(string endpointUrl, string pipeName)
        {
            throw new NotImplementedException();
        }

        public abstract void Close();

        protected static Assembly LoadFromSameFolder(object sender, ResolveEventArgs args)
        {
            Assembly assembly = null;

            try
            {
                var folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (folderPath == null) return null;

                var assemblyPath = Path.Combine(folderPath, string.Format("{0}.dll", new AssemblyName(args.Name).Name));

                if (File.Exists(assemblyPath) == false) return null;

                assembly = Assembly.LoadFrom(assemblyPath);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            return assembly;
        }
    }
}