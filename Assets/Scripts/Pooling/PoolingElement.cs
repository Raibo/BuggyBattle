using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Pooling
{
    public readonly struct PoolingElement
    {
        public readonly GameObject GameObject;
        public readonly Poolable Poolable;


        public PoolingElement(GameObject gameObject, Poolable poolable)
        {
            GameObject = gameObject;
            Poolable = poolable;
        }
    }
}
