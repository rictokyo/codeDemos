using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyResolver.DynamicLinq
{
    internal class Signature : IEquatable<Signature>
    {
        public int hashCode;
        public DynamicProperty[] properties;

        public Signature(IEnumerable<DynamicProperty> properties)
        {
            this.properties = properties.ToArray();
            this.hashCode = 0;
            foreach (DynamicProperty p in properties)
            {
                this.hashCode ^= p.Name.GetHashCode() ^ p.Type.GetHashCode();
            }
        }

        public bool Equals(Signature other)
        {
            if (this.properties.Length != other.properties.Length) return false;
            for (int i = 0; i < this.properties.Length; i++)
            {
                if (this.properties[i].Name != other.properties[i].Name ||
                    this.properties[i].Type != other.properties[i].Type) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return this.hashCode;
        }

        public override bool Equals(object obj)
        {
            return obj is Signature ? Equals((Signature) obj) : false;
        }
    }
}