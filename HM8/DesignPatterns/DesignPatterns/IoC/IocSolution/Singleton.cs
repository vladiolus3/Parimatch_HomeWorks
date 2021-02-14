using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC.IocSolution
{
    public class Singleton<T> : IEntity
    {
        private static Singleton<T> _instance;

        private static object _objectType;

        private static List<Type> _types = null;

        private static readonly Type Type = typeof(T);

        public Singleton()
        {
        }

        public IEntity GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton<T>();
                _objectType = Helper.GenerateConstructor<T>(_types);
            }

            return _instance;
        }

        public object GetConstructor() => _objectType;

        public void SetParams(List<Type> types) => _types = types;

        public Type GetEntityType() => Type;
    }
}
