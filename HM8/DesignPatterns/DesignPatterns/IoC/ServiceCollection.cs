using System;
using DesignPatterns.IoC.IocSolution;

namespace DesignPatterns.IoC
{
    public class ServiceCollection : IServiceCollection
    {

        private readonly ServiceProvider _serviceProvider = new ServiceProvider();

        public IServiceCollection AddTransient<T>()
        {
            if (typeof(T).IsValueType)
            {
                throw new TypeAccessException("Argument must be the reference type");
            }

            var transient = new Transient<T>();

            _serviceProvider.AddEntity(transient);

            return this;
        }

        public IServiceCollection AddTransient<T>(Func<T> factory)
        {
            return AddTransient<T>();
        }

        public IServiceCollection AddTransient<T>(Func<IServiceProvider, T> factory)
        {
            return AddTransient<T>();
        }

        public IServiceCollection AddSingleton<T>()
        {
            if (typeof(T).IsValueType)
            {
                throw new TypeAccessException("Argument must be the reference type");
            }

            var singleton = new Singleton<T>();

            _serviceProvider.AddEntity(singleton);

            return this;
        }

        public IServiceCollection AddSingleton<T>(T service)
        {
            return AddSingleton<T>();
        }

        public IServiceCollection AddSingleton<T>(Func<T> factory)
        {
            return AddSingleton<T>();
        }

        public IServiceCollection AddSingleton<T>(Func<IServiceProvider, T> factory)
        {
            return AddSingleton<T>();
        }

        public IServiceProvider BuildServiceProvider()
        {
            return _serviceProvider;
        }
    }
}