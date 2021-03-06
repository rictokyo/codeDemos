using System;

namespace StrategyResolver.DynamicLinq
{
    public class DynamicProperty
    {
        private readonly string name;
        private readonly Type type;

        public DynamicProperty(string name, Type type)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (type == null) throw new ArgumentNullException("type");
            this.name = name;
            this.type = type;
        }

        public string Name
        {
            get { return this.name; }
        }

        public Type Type
        {
            get { return this.type; }
        }
    }
}