using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SampleBundleApi.Misc
{
    /// <summary>
    /// xml handling helper class to help handling xml 
    /// </summary>
    public static class XmlHandlingHelper
    {
        public static T GetObjectFromXml<T>(string displaySectionRulesXml)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(displaySectionRulesXml)))
            {
                return (T)serializer.Deserialize(ms);
            }
        }
    }
}