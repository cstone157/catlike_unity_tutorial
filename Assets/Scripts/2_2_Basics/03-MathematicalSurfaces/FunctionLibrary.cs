using UnityEngine;
using static UnityEngine.Mathf;

namespace MathematicalSurfaces
{
    public static class FunctionLibrary {
        // Define the Wave function for our library (using sine)
        public static float Wave (float x, float t) {
            return Sin(PI * (x + t));
        }

        // Define the function for our library, that is the multiwave
        public static float MultiWave (float x, float t) {
            //float y = Sin(PI * (x + t));
            float y = Sin(PI * (x + 0.5f * t));
            y += 0.5f * Sin(2f * PI * (x + t));
		    return y * (2f / 3f);
        }

        // Define a ripple funtion for our library
        public static float Ripple (float x, float t) {
            float d = Abs(x);
            float y = Sin(4f * PI * d);
            return y;
        }
    }
}