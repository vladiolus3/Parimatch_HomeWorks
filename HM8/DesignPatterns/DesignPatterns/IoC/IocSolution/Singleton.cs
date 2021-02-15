using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC.IocSolution
{
    public class Singleton<T> : IEntity
    {
        private static object _objectType;

        private static List<Type> _types = null;

        private static readonly Type Type = typeof(T);

        public Singleton()
        {
        }

        public object GetInstance()
        {
            if (_objectType == null)
            {
                _objectType = Helper.GenerateConstructor<T>(_types);
            }

            return _objectType;
        }

        public void SetParams(List<Type> types) => _types = types;

        public Type GetEntityType() => Type;
    }
}
