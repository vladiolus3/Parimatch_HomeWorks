using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC.IocSolution
{
    public interface IEntity
    {
        public object GetInstance();

        public void SetParams(List<Type> types);

        public Type GetEntityType();
    }
}