using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleBundleApi;
using SampleBundleApi.Misc;
using StrategyResolver.Strategy;
using StrategyResolver.Strategy.Models;

namespace SampleTests
{
    [TestClass]
    public class StreategyResolverTests
    {
       [TestMethod]
       [DeploymentItem(@"Data\SampleDynamicStrategy.xml", "Data")]
        public void TestResolveStrategy()
        {
            var xml = ResourceUtils.GetResourceTextFromFile("SampleDynamicStrategy.xml");
            var rules = XmlHandlingHelper.GetObjectFromXml<Rules>(xml);
            var model = new RuleValidateInfo();

            var resolver = new SimpleDynamicStrategyResolver<RuleValidateInfo>();
            var sampleDynamicStrategies = resolver.GetResolvedValidatorsGeneric(rules);

            foreach (var strategy in sampleDynamicStrategies.Where(strategy => strategy.Validate(model)))
            {
                Console.WriteLine(strategy.Message);
            }
        }

        public class RuleValidateInfo : IRuleValidateInfo
        {
            public bool IsStatusActive
            {
                get { return true; }
            }
        }
    }
}
