using System.Xml.Serialization;

namespace StrategyResolver.Strategy.Models
{
    public class Rule : IRule
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("executionOrder")]
        public int ExecutionOrder { get; set; }
        [XmlAttribute("validation")]
        public string ValidationLinq { get; set; }
        [XmlAttribute("message")]
        public string Message { get; set; }
    }
}