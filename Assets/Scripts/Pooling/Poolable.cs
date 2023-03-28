using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Pooling
{
    public class Poolable : MonoBehaviour
    {
        public Pool Pool;


        [ContextMenu("Return")]
        public void Return()
        {
            Pool.Return(gameObject, this);
        }
    }
}
