using System.Collections.Generic;

namespace FactoryPool
{
    public class Pool<T> : IPool<T> where T : IPooledObject
    {
        private readonly IFactory<T> factory;
        private readonly List<T> pooledObjects = new List<T>();

        public Pool(IFactory<T> factory, int amountObject)
        {
            this.factory = factory;
            for (var i = 0; i < amountObject; i++)
            {
                pooledObjects.Add(NewPoolObject());
            }
        }

        public T Pull()
        {
            if (pooledObjects.Count == 0)
            {
                pooledObjects.Add(NewPoolObject());
            }

            var returnValue = pooledObjects[0];
            returnValue.Initialize(); 
            pooledObjects.Remove(returnValue);
            return returnValue;
        }

        public void Push(IPooledObject pooledObject)
        {
            pooledObjects.Add((T) pooledObject);
        }

        private T NewPoolObject()
        {
            var returnValue = factory.CreateObject();
            returnValue.SetParentPool(this);
            return returnValue;
        }
    }   
}