using System.Collections.Generic;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Pooling
{
    public class Pool : MonoBehaviour
    {
        public GameObject Prefab;
        public int MinimumCapacity;

        private Stack<PoolingElement> _elements;
        private Transform _transform;


        private void Start()
        {
            _transform = transform;
            _elements = new Stack<PoolingElement>(MinimumCapacity);

            for (int i = 0; i < MinimumCapacity; i++)
                _elements.Push(CreateNewElement());
        }


        public PoolingElement Rent()
        {
            if (_elements.Count <= 0)
                _elements.Push(CreateNewElement());

            return _elements.Pop();
        }


        public void Return(GameObject obj, Poolable poolable) =>
            Return(new(obj, poolable));


        public void Return(PoolingElement element)
        {
            element.GameObject.SetActive(false);
            element.GameObject.transform.SetParent(_transform);
            _elements.Push(element);
        }


        private PoolingElement CreateNewElement()
        {
            var newObj = Instantiate(Prefab, _transform);
            newObj.SetActive(false);

            var poolable = newObj.GetComponent<Poolable>() ?? newObj.AddComponent<Poolable>();
            poolable.Pool = this;

            return new(newObj, poolable);
        }
    }
}
