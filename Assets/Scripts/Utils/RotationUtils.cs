using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Utils
{
    public static class RotationUtils
    {
        public static Vector3 RotateAround(Vector3 point, Vector3 pivotPoint, Quaternion rot) =>
            rot * (point - pivotPoint) + pivotPoint;


        public static Quaternion RotationToPointByAxis(Transform rotated, Vector3 worldPoint, Vector3 localAxis)
        {
            var localPoint = rotated.InverseTransformPoint(worldPoint);
            var localPointProjected = Vector3.ProjectOnPlane(localPoint, localAxis);

            var adjustingRotation = Quaternion.FromToRotation(Vector3.forward, localPointProjected);
            var endRotation = adjustingRotation * rotated.localRotation;

            return endRotation;
        }
    }
}
