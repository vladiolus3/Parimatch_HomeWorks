using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC.IocSolution
{
    public class Transient<T> : IEntity
    {
        private static Transient<T> _instance;

        private static object _objectType;

        private static List<Type> _types = null;

        private static readonly Type Type = typeof(T);

        public Transient()
        {
        }

        public IEntity GetInstance()
        {
            _instance = new Transient<T>();
            _objectType = Helper.GenerateConstructor<T>(_types);

            return _instance;
        }

        public object GetConstructor() => _objectType;

        public void SetParams(List<Type> types) => _types = types;

        public Type GetEntityType() => Type;
    }
}
