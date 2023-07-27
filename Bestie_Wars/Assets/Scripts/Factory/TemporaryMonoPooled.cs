﻿using System;
using System.Collections;
using UnityEngine;

namespace FactoryPool
{
    public class TemporaryMonoPooled : MonoBehaviour, IPooledObject
    {
        [SerializeField] private float timer;
        private IPool pool;

        public event Action<TemporaryMonoPooled> ReadyToReturnPool;

        protected float Timer
        {
            set => timer = value;
        }

        public virtual void Initialize()
        {
            if (this == null)
            {
                return;
            }
            gameObject.SetActive(true);
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(Destroy());
            }
        }

        public void ReturnToPool()
        {
            ReadyToReturnPool?.Invoke(this);
            gameObject.SetActive(false);
            pool.Push(this);
        }

        public void SetParentPool<T>(IPool<T> parent) where T : IPooledObject
        {
            pool = parent;
        }

        protected IEnumerator Destroy()
        {
            yield return new WaitForSeconds(timer);
            ReturnToPool();
        }
    }   
}
