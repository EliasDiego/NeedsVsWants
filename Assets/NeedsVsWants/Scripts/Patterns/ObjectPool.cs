using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace NeedsVsWants.Patterns
{
    [System.Serializable]
    public class ObjectPool
    {
        [SerializeField]
        string _Id;
        [SerializeField]
        GameObject _Prefab;
        [SerializeField]
        int _Count;
        [SerializeField]
        bool _IsExpandable;
        
        Transform _Parent;

        bool _IsInstantiated = false;

        Queue<ObjectPoolInstance> _ActivePool = new Queue<ObjectPoolInstance>();
        Stack<ObjectPoolInstance> _InactivePool = new Stack<ObjectPoolInstance>();

        public Transform parent => _Parent;
        
        public string id => _Id;

        public bool isInstantiated => _IsInstantiated;

        ObjectPoolInstance InstantiatePrefab()
        {
            GameObject gameObject = GameObject.Instantiate(_Prefab, _Parent);
            ObjectPoolInstance instance;

            gameObject.transform.SetParent(_Parent);
            gameObject.SetActive(false);

            instance = gameObject.AddComponent<ObjectPoolInstance>();
            instance.SetObjectPool(this);

            return instance;
        }

        public void Instantiate()
        {   
            ObjectPoolInstance instance;
            
            _Parent = new GameObject(_Id + " Pool").transform;

            _IsInstantiated = true;
                
            for(int i = 0; i < _Count; i++)
            {
                instance = InstantiatePrefab();

                _InactivePool.Push(instance);
            }
        }

        public void Destroy() 
        {
            foreach(ObjectPoolInstance instance in _ActivePool)
                GameObject.Destroy(instance.gameObject);

            if(_Parent)
                GameObject.Destroy(_Parent.gameObject);

            _IsInstantiated = false;

            _ActivePool.Clear();
            _InactivePool.Clear();
        }

        public GameObject GetObject(bool getFromActivePoolIfEmpty = false)
        {
            ObjectPoolInstance instance = null;

            if(_InactivePool.Count <= 0)
            {
                if(_IsExpandable)
                    instance = InstantiatePrefab();

                else if(getFromActivePoolIfEmpty)
                    instance = _ActivePool.Dequeue();
            }

            else
                instance = _InactivePool.Pop();

            instance.ResetInstance();
            instance.transform.SetParent(null);
            instance.gameObject.SetActive(true);

            _ActivePool.Enqueue(instance);

            return instance.gameObject;
        }

        public GameObject[] GetObjects(int count, bool getFromActivePoolIfEmpty = false)
        {
            List<ObjectPoolInstance> instanceList = new List<ObjectPoolInstance>();

            ObjectPoolInstance instance = null;

            for(int i = 0; i < count; i++)
            {
                if(_InactivePool.Count <= 0)
                {
                    if(_IsExpandable)
                        instance = InstantiatePrefab();

                    else if(getFromActivePoolIfEmpty)
                        instance = _ActivePool.Dequeue();
                }

                else
                    instance = _InactivePool.Pop();

                instanceList.Add(instance);
                    
                instance.ResetInstance();
                instance.transform.SetParent(null);
                instance.gameObject.SetActive(true);
            }

            for(int i = 0; i < instanceList.Count; i++)
                _ActivePool.Enqueue(instanceList[i]);

            return instanceList.Select(i => i.gameObject).ToArray();
        }

        async public void Return(ObjectPoolInstance instance)
        {
            if(instance.objectPool == this)
            {
                _ActivePool = new Queue<ObjectPoolInstance>(_ActivePool.Where(i => i != instance));

                _InactivePool.Push(instance);
                
                await System.Threading.Tasks.Task.Delay(1);

                Debug.Log(instance != null);
                
                instance.ParentToPool();
            }
        }
    }
}