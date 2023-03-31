using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Unils
{
    public static class VectorUtils
    {
        public static Vector3 RotateAround(Vector3 point, Vector3 pivotPoint, Quaternion rot) =>
            rot * (point - pivotPoint) + pivotPoint;
    }
}
