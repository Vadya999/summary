using System;

namespace FactoryPool
{
    public class Factory<T> : IFactory<T>
    {
        private readonly object[] args;

        public Factory(params object[] args)
        {
            this.args = args;
        }
        
        public T CreateObject()
        {
            return (T) Activator.CreateInstance(typeof(T), args);
        }
    }   
}