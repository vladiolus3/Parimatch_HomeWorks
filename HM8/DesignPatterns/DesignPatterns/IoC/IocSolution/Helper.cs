using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DesignPatterns.IoC.IocSolution
{
    public static class Helper
    {
        public static object GenerateConstructor<T>(List<Type> types) => GenerateConstructor(typeof(T), types);

        public static object GenerateConstructor(Type inResult, List<Type> types)
        {
            var constructors = inResult.GetConstructors();
            var paramTypes = constructors.Select(MethodSignature).ToArray();

            for (var i = 0; i < constructors.Length; i++)
            {
                var listParam = new List<object>();

                try
                {
                    for (var j = 0; j < paramTypes[i].Length; j++)
                    {
                        try
                        {
                            var instance = Activator.CreateInstance(paramTypes[i][j]);
                            listParam.Add(instance);
                        }
                        catch (MissingMethodException)
                        {
                            listParam.Add(GenerateConstructor(paramTypes[i][j], types));
                        }

                    }

                    var result = Activator.CreateInstance(inResult, listParam.ToArray());
                    return result;
                }
                catch (MissingMethodException)
                {
                }

            }

            throw new MissingMethodException("no matching constructor found");
        }

        private static Type[] MethodSignature(this ConstructorInfo mi)
        {
            var param = mi.GetParameters()
                .Select(p => p.ParameterType.FullName)
                .ToArray();

            return param.Select(x => Type.GetType(x)).ToArray();
        }
    }
}
