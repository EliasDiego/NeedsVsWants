using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using UnityEditor;

namespace NeedsVsWants.Patterns
{
    public class ObjectPoolManager : SimpleSingleton<ObjectPoolManager>
    {
        [SerializeField]
        ObjectPool[] _ObjectPools;

#if UNITY_EDITOR
        [MenuItem("GameObject/Patterns/Object Pool Manager", false, 10)]
        static void CreateObjectPoolManager(MenuCommand menuCommand)
        {
            ObjectPoolManager objectPoolManager = new GameObject("ObjectPoolManager").AddComponent<ObjectPoolManager>();
        }
#endif
        public void Instantiate()
        {
            foreach(ObjectPool objectPool in _ObjectPools)
            {
                if(!objectPool.isInstantiated)
                {
                    objectPool.Instantiate();

                    objectPool.parent.SetParent(transform);
                }

                else
                    Debug.LogWarning("ObjectPoolWarning: " + objectPool.id + " Pool is already instantiated!");
            }
        }

        public void Instantiate(string id)
        {
            ObjectPool objectPool = _ObjectPools.First(o => o.id == id);

            if(objectPool != null && !objectPool.isInstantiated)
                objectPool.Instantiate();
        }

        public void Destroy()
        {
            if(_ObjectPools != null)
            {
                foreach(ObjectPool objectPool in _ObjectPools)
                    objectPool.Destroy();
            }
        }

        public void Destroy(string id)
        {
            _ObjectPools.First(o => o.id == id)?.Destroy();
        }

        public GameObject GetObject(string id, bool getFromActivePoolIfEmpty = false) 
        {
            ObjectPool objectPool = null;

            try
            {
                objectPool = _ObjectPools.First(pool => pool.id == id);
            }

            catch (Exception e)
            {
                Debug.LogError("GetObjectError: Object Pool ID " + '\"' + id + '\"' + " not found!\n" + e);
            }

            return objectPool == null ? null : objectPool.GetObject(getFromActivePoolIfEmpty);
        }

        public GameObject[] GetObjects(string id, int count, bool getFromActivePoolIfEmpty = false)
        {
            ObjectPool objectPool = null;

            try
            {
                objectPool = _ObjectPools.First(pool => pool.id == id);
            }

            catch (Exception e)
            {
                Debug.LogError("GetObjectsError: Object Pool ID " + '\"' + id + '\"' + " not found!\n" + e);
            }

            return objectPool == null ? null : objectPool.GetObjects(count, getFromActivePoolIfEmpty);
        }
    }
}