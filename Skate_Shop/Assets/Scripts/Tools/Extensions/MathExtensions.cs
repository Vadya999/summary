using UnityEngine;

namespace UnityTools.Extentions
{
    public static class MathExtensions
    {
        public static float PingPong(this float value, float length)
        {
            return Mathf.PingPong(value, length);
        }

        public static float Repeat(this float value, float length)
        {
            return Mathf.Repeat(value, length);
        }

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static float Clamp(this float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }

        public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            var AB = b - a;
            var AV = value - a;
            return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
        }
    }
}