using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SampleBundleApi.Misc
{
    public static class ResourceUtils
    {
        public static string GetResourceTextFromFile(string filename)
        {
            var result = string.Empty;
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;

            if (type == null) return result;

            var manifestResourceNames = type.Assembly.GetManifestResourceNames();
            var resourceStreamName = manifestResourceNames.Where(y => y.EndsWith(string.Format(".{0}", filename)));

            using (var stream = type.Assembly.GetManifestResourceStream(resourceStreamName.Single()))
            {
                Debug.Assert(stream != null, "stream != null");

                using (var sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }

            return result;
        }
    }
}