using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.Patterns
{
    public class ObjectPoolInstance : MonoBehaviour
    {       
        IOnObjectPoolEvent _EventHandler;

        ObjectPool _ObjectPool;

        public ObjectPool objectPool => _ObjectPool;

        void Awake() => _EventHandler = GetComponentInChildren<IOnObjectPoolEvent>();

        void OnDisable() => Invoke("ReturnToPool", 0.1f);

        public void SetObjectPool(ObjectPool objectPool) => _ObjectPool = objectPool;
        
        public void ResetInstance() => _EventHandler?.OnReset();

        public void ReturnToPool()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);

            transform.parent = _ObjectPool.parent;
            
            //_ObjectPool.Return(this);
        }

        public void ParentToPool() => transform.parent = _ObjectPool.parent;
}
}
