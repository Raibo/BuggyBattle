using System.Collections.Generic;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Pooling
{
    public class PoolsCarrier : MonoBehaviour
    {
        public static PoolsCarrier Instance => GetInstance();
        private Dictionary<GameObject, Pool> _poolMap;
        private static PoolsCarrier _instance;


        private void OnEnable() =>
            InitPoolMap();


        private void OnDestroy()
        {
            _poolMap.Clear();
            _poolMap = null;
        }


        public Pool GetPoolForPrefab(GameObject prefab)
        {
            InitPoolMap();
            return _poolMap.TryGetValue(prefab, out var pool) ? pool : null;
        }


        private void InitPoolMap()
        {
            if (_poolMap is not null)
                return;

            _poolMap = new();

            foreach (var pool in transform.GetComponentsInChildren<Pool>())
                _poolMap[pool.Prefab] = pool;
        }


        private static PoolsCarrier GetInstance()
        {
            if (_instance == null)
                _instance = FindObjectOfType<PoolsCarrier>();

            return _instance;
        }
    }
}
