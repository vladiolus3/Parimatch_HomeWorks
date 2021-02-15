using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC.IocSolution
{
    public class Transient<T> : IEntity
    {
        private static object _objectType;

        private static List<Type> _types = null;

        private static readonly Type Type = typeof(T);

        public Transient()
        {
        }

        public object GetInstance()
        {
            _objectType = Helper.GenerateConstructor<T>(_types);

            return _objectType;
        }

        public void SetParams(List<Type> types) => _types = types;

        public Type GetEntityType() => Type;
    }
}
