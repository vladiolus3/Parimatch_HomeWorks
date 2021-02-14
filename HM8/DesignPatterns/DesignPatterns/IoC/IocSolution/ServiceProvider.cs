using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.IoC.IocSolution
{
    class ServiceProvider : IServiceProvider
    {
        private readonly List<IEntity> _listOfEntities = new List<IEntity>();

        public ServiceProvider()
        {

        }

        public void AddEntity(IEntity entity)
        {
            if (_listOfEntities.Any(x => x.GetEntityType() == entity.GetEntityType()))
            {
                throw new ArgumentOutOfRangeException("such an element is already in the collection!");
            }

            _listOfEntities.Add(entity);
        }

        public T GetService<T>()
        {
            if (_listOfEntities.All(x => (x).GetEntityType() != typeof(T)))
            {
                return default;
            }

            var entity = _listOfEntities.First(x => x.GetEntityType() == typeof(T));

            List<Type> list = new List<Type>();
            foreach (var x in _listOfEntities) list.Add(x.GetEntityType());

            list.AddRange(new List<Type>()
                {
                    typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint),
                    typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal), typeof(char),
                    typeof(bool), typeof(object), typeof(string), typeof(DateTime)
                }
            );

            entity.SetParams(list);
            entity.GetInstance();

            return (T)entity.GetConstructor();
        }
    }
}
