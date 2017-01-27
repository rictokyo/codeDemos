using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StrategyResolver.Strategy.Models
{
    [Serializable]
    public abstract class RuleListContainer<T> : RuleContainer<T> where T : IRule
    {
        [XmlElement(ElementName = "Rule")]
        public List<T> RulesList { get; set; }

        public override IEnumerable<T> GetRules()
        {
            return this.RulesList;
        }
    }
}