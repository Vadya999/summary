using System;
using UnityEngine;

namespace FactoryPool
{
    public class FactoryMonoObject<T> : IFactory<T>
    {
        private readonly GameObject prefab;
        public Transform Parent { get; private set; }

        public FactoryMonoObject(GameObject prefab, Transform parent, bool isParent = true)
        {
            Parent = parent;
            this.prefab = prefab;
            if (isParent)
            {
                var newParent = new GameObject();
                newParent.transform.parent = parent;
                Parent = newParent.transform;
                Parent.localScale = Vector3.one;
                Parent.name = prefab.name;
            }
        }

        public T CreateObject()
        {
            var newObject = GameObject.Instantiate(prefab, Parent);
            var returnValue = newObject.GetComponent<T>();
            newObject.SetActive(false);
            if (returnValue != null)
            {
                return returnValue;
            }

            throw new InvalidOperationException(
                $"The requested object is missing from the prefab {typeof(T)} >> {prefab.name}");
        }
    }
}