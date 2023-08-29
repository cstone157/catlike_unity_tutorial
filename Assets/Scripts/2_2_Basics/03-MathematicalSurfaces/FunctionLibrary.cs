using UnityEngine;
using static UnityEngine.Mathf;

namespace MathematicalSurfaces
{
    public static class FunctionLibrary {
        // Delegate is a special type that defines what kind of method something can reference
        public delegate float Function (float x, float z, float t);

        // An enumeration of the function names
        public enum FunctionName { Wave, MultiWave, Ripple }

        // An array of functions rather than using numbers
        static Function[] functions = { Wave, MultiWave, Ripple };

        // Get the function asked for
        public static Function GetFunction (FunctionName name) {
            return functions[(int)name];
        }

        // Define the Wave function for our library (using sine)
        public static float Wave (float x, float z, float t) {
            return Sin(PI * (x + z + t));
        }

        // Define the function for our library, that is the multiwave
        public static float MultiWave (float x, float z, float t) {
            //float y = Sin(PI * (x + t));
            float y = Sin(PI * (x + 0.5f * t));
            y += 0.5f * Sin(2f * PI * (z + t));
            y += Sin(PI * (x + z + 0.25f * t));
		    return y * (1f / 2.5f);
        }

        // Define a ripple funtion for our library
        public static float Ripple (float x, float z, float t) {
            float d = Sqrt(x * x + z * z);
            float y = Sin(PI * (4f * d - t));
            return y / (1f + 10f * d);
        }
    }
}