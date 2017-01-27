using System;

namespace StrategyResolver.Strategy
{
    public static class ReflectionHelper
    {
        public static T GetTypeFromString<T>(string assemblyName, string typeName)
        {
            var instance = default(T);

            var fullTypeName = string.Format("{0}, {1}", typeName, assemblyName);
            var resolvedType = Type.GetType(fullTypeName);

            if (resolvedType != null)
            {
                instance = (T)Activator.CreateInstance(resolvedType);
            }

            return instance;
        }

        public static T GetGenericTypeFromString<T, T1>(string assemblyName, string typeName)
        {
            var instance = default(T);

            var fullTypeName = string.Format("{0}, {1}", typeName, assemblyName);
            var resolvedType = Type.GetType(fullTypeName);
            var genericType = typeof(T1);
            Type[] typeArgs = { genericType };

            if (resolvedType != null)
            {
                var repositoryType = resolvedType.MakeGenericType(typeArgs);
                instance = (T)Activator.CreateInstance(repositoryType);
            }

            return instance;
        }
    }
}