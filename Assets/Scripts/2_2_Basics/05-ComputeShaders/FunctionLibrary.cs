using UnityEngine;
using static UnityEngine.Mathf;

namespace ComputeShaders
{
    public static class FunctionLibrary {
        // Delegate is a special type that defines what kind of method something can reference
        //public delegate float Function (float x, float z, float t);
        public delegate Vector3 Function (float u, float v, float t);


        // An enumeration of the function names
        public enum FunctionName { Wave, MultiWave, Ripple, Sphere, RotatingSphere, Torus }
        
        // An array of functions rather than using numbers
        static Function[] functions = { Wave, MultiWave, Ripple, Sphere, RotatingSphere, Torus };

        // Get the function asked for
        public static Function GetFunction (FunctionName name) {
            return functions[(int)name];
        }

        // Get the next function name for our loop
        public static FunctionName GetNextFunctionName (FunctionName name) {
            return (int)name < functions.Length - 1 ? name + 1 : 0;
        }

        // Get the next function (but not the current one)
        public static FunctionName GetRandomFunctionNameOtherThan (FunctionName name) {
            var choice = (FunctionName)Random.Range(1, functions.Length);
            return choice == name ? 0 : choice;
        }

        // Get a Random Function Name
        public static FunctionName GetRandomFunctionName () {
            var choice = (FunctionName)Random.Range(0, functions.Length);
            return choice;
        }

        // Morph from one function to another
        public static Vector3 Morph (float u, float v, 
            float t, Function from, Function to, float progress) {
            //return Vector3.Lerp(from(u, v, t), to(u, v, t), progress);
            //return Vector3.Lerp(from(u, v, t), to(u, v, t), SmoothStep(0f, 1f, progress));
            return Vector3.LerpUnclamped(
                from(u, v, t), to(u, v, t), SmoothStep(0f, 1f, progress)
            );
        }

        // Define the Wave function for our library (using sine)
        public static Vector3 Wave (float u, float v, float t) {
            Vector3 p;
            p.x = u;
            p.y = Sin(PI * (u + v + t));
            p.z = v;
            return p;
        }

        // Define the function for our library, that is the multiwave
        public static Vector3 MultiWave (float u, float v, float t) {
            Vector3 p;
            p.x = u;
            p.y = Sin(PI * (u + 0.5f * t));
            p.y += 0.5f * Sin(2f * PI * (v + t));
            p.y += Sin(PI * (u + v + 0.25f * t));
            p.y *= 1f / 2.5f;
            p.z = v;
            return p;
        }

            // Define a ripple funtion for our library
        public static Vector3 Ripple (float u, float v, float t) {
            float d = Sqrt(u * u + v * v);
            Vector3 p;
            p.x = u;
            p.y = Sin(PI * (4f * d - t));
            p.y /= 1f + 10f * d;
            p.z = v;
            return p;
        }
        
        public static Vector3 Sphere (float u, float v, float t) {
            // Plain Sphere
            float r = Cos(0.5f * PI * v);
            Vector3 p;
            p.x = r * Sin(PI * u);
            p.y = Sin(PI * 0.5f * v);
            p.z = r * Cos(PI * u);
            return p;
        }
        public static Vector3 RotatingSphere (float u, float v, float t) {
            float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
            float s = r * Cos(0.5f * PI * v);
            Vector3 p;
            p.x = s * Sin(PI * u);
            p.y = r * Sin(0.5f * PI * v);
            p.z = s * Cos(PI * u);
            return p;
        }

        public static Vector3 Torus (float u, float v, float t) {
            float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
            float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
            float s = r1 + r2 * Cos(PI * v);
            Vector3 p;
            p.x = s * Sin(PI * u);
            p.y = r2 * Sin(PI * v);
            p.z = s * Cos(PI * u);
            return p;
        }
    }
}