using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StrategyResolver.Strategy.Models
{
    [Serializable]
    public abstract class RuleContainer<T> : IRuleContainer<T> where T : IRule
    {
        public abstract IEnumerable<T> GetRules();
        [XmlAttribute("namespace")]
        public virtual string Namespace { get; set; }
        [XmlAttribute("assemblyname")]
        public virtual string AssemblyName { get; set; }
    }
}