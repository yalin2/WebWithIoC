using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyIocContainer
{
    static public class MyIocContainer
    {
        public enum LifestyleType { Transient, Singleton };
        private static readonly Dictionary<Type, TypeResolution> types = new Dictionary<Type, TypeResolution>();
        private class TypeResolution
        {
            public Type ToType { get; set; }
            public MyIocContainer.LifestyleType LifestyleType { get; set; }
            public object StaticInstance { get; set; }
            public TypeResolution(Type toType, MyIocContainer.LifestyleType lifestyleType = MyIocContainer.LifestyleType.Transient)
            {
                ToType = toType;
                LifestyleType = lifestyleType;
                StaticInstance = null;
            }
        }
        public static void Register<TFromType, TToType>(LifestyleType lifestyleType = LifestyleType.Transient)
        {
            var mapping = types.FirstOrDefault(t=>t.Key.Equals(typeof(TFromType))).Value;
            if (mapping == null || mapping.ToType != typeof(TToType))
            {
                types[typeof(TFromType)] = new TypeResolution(typeof(TToType), lifestyleType);
            }
        }   
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        public static object Resolve(Type typeToResolve)
        {
            try
            {
                Type resolvedType = types[typeToResolve].ToType;
                ConstructorInfo constructor = resolvedType.GetConstructors()[0];
                ParameterInfo[] constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length == 0)
                {
                    if (types[typeToResolve].LifestyleType == LifestyleType.Singleton && types[typeToResolve].StaticInstance != null)
                    {
                        return types[typeToResolve].StaticInstance;
                    }
                    else
                    {
                        types[typeToResolve].StaticInstance = Activator.CreateInstance(resolvedType);
                        return Activator.CreateInstance(resolvedType);
                    }
                }
                else
                {
                    List<object> parameters = new List<object>(constructorParameters.Length);
                    foreach (ParameterInfo parameterInfo in constructorParameters)
                    {
                        parameters.Add(Resolve(parameterInfo.ParameterType));
                    }
                    if (types[typeToResolve].LifestyleType == LifestyleType.Singleton 
                        && types[typeToResolve].StaticInstance != null)
                    {
                        return types[typeToResolve].StaticInstance;
                    }
                    else
                    {
                        types[typeToResolve].StaticInstance = constructor.Invoke(parameters.ToArray());
                        return constructor.Invoke(parameters.ToArray());
                    }
                }
            }
            catch
            {
                throw new InvalidOperationException("Trying to resolve an invalid type. Valid Types are: " + string.Join(", ", types.Select(t=>t.Key.ToString()).ToArray()));
            }
        }
    }
    
}
