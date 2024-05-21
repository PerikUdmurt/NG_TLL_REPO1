using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyPetProject.ObjectPool
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private T _prefab;
        private List<T> _objects;
        public ObjectPool(T prefab, int prepareObjects)
        {
            _prefab = prefab;
            _objects = new List<T>();

            for (int i = 0; i < prepareObjects; i++)
            {
                Create(prefab);
            }
        }


        public T Get()
        {
            var obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

            if (obj == null)
            {
                obj = Create(_prefab);
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        private T Create(T prefab)
        {
            var obj = GameObject.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            _objects.Add(obj);
            return obj;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
