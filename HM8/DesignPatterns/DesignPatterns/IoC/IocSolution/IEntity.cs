using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC.IocSolution
{
    public interface IEntity
    {
        public IEntity GetInstance();

        public void SetParams(List<Type> types);

        public object GetConstructor();

        public Type GetEntityType();
    }
}