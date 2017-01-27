using System;
using System.Xml.Serialization;

namespace StrategyResolver.Strategy.Models
{
    [XmlRoot(ElementName = "Rules")]
    [Serializable]
    public class Rules : RuleListContainer<Rule>
    {
      
    }
}