using NUnit.Framework;
using Unity.Mathematics;

namespace Timespawn.Core.Tests
{
    public static class TestUtils
    {
        private const double APPROX_TOLERANCE = 0.0001d;

        public static void AreApproximatelyEqual(double expected, double actual, string message = "", double tolerance = APPROX_TOLERANCE)
        {
            Assert.AreEqual(expected, actual, tolerance, message);
        }

        public static void AreApproximatelyEqual(quaternion expected, quaternion actual, string message = "", double tolerance = APPROX_TOLERANCE)
        {
            AreApproximatelyEqual(expected.value.x, actual.value.x, message, tolerance);
            AreApproximatelyEqual(expected.value.y, actual.value.y, message, tolerance);
            AreApproximatelyEqual(expected.value.z, actual.value.z, message, tolerance);
            AreApproximatelyEqual(expected.value.w, actual.value.w, message, tolerance);
        }

        public static void AreApproximatelyEqualFloat3(float3 expected, float3 actual, string message = "", double tolerance = APPROX_TOLERANCE)
        {
            AreApproximatelyEqual(expected.x, actual.x, message, tolerance);
            AreApproximatelyEqual(expected.y, actual.y, message, tolerance);
            AreApproximatelyEqual(expected.z, actual.z, message, tolerance);
        }
    }
}