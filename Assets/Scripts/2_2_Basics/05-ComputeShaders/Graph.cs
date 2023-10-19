using UnityEngine;

using ComputeShaders;

/**
 * NOTE: Skipped section 3.4 - Better Visuals.
 *      I wasn't able to find the shadow setting section, doesn't matter
 *      so I skipped it.
**/
namespace ComputeShaders
{
    public class Graph : MonoBehaviour {

        // The prefab of our point object
        [SerializeField]
        Transform pointPrefab;

        // Number of cubes to create
        [SerializeField, Range(10, 100)]
        int resolution = 10;

        // Select a function to move the points by
        [SerializeField]
	    FunctionLibrary.FunctionName function;
        
        public enum TransitionMode { Cycle, Random }

        [SerializeField]
        TransitionMode transitionMode;

        [SerializeField, Min(0f)]
        float functionDuration = 1f, transitionDuration = 1f;

        Transform[] points;
        float duration;
        bool transitioning;
        FunctionLibrary.FunctionName transitionFunction;

        // On the object awakening
        void Awake () {
            // Initial all of our variables
            float step = 2f / resolution;
            var scale = Vector3.one * step;
            
            points = new Transform[resolution * resolution];
            for (int i = 0; i < points.Length; i++) {
                // Create the transform for all of our points
                Transform point = points[i] = Instantiate(pointPrefab);

                // Assign the x position (since this never changes this is here, not in the update)
                point.localScale = scale;
                point.SetParent(transform, false);

                // When a new parent is set Unity will attempt to keep 
                //  the object at its original world position, rotation, 
                //  and scale. We don't need this in our case. We can 
                //  signal this by passing false as a second argument 
                //  to SetParent.
                point.SetParent(transform, false);
            }
        }

        // On the object updating
        void Update () {
            duration += Time.deltaTime;
            if (transitioning) {
                if (duration >= transitionDuration) {
                    duration -= transitionDuration;
                    transitioning = false;
                }
            }
    		else if (duration >= functionDuration) {
                duration -= functionDuration;
                //function = FunctionLibrary.GetNextFunctionName(function);
                transitioning = true;
                transitionFunction = function;

                PickNextFunction();
            }

            if (transitioning) {
                UpdateFunctionTransition();
            }
            else {
                UpdateFunction();
            }
        }

        void PickNextFunction () {
            function = transitionMode == TransitionMode.Cycle ?
                FunctionLibrary.GetNextFunctionName(function) :
                FunctionLibrary.GetRandomFunctionNameOtherThan(function);
        }

        void UpdateFunction () {
            FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
            float time = Time.time;
            float step = 2f / resolution;
            float v = 0.5f * step - 1f;
            for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++) {
                if (x == resolution) {
                    x = 0;
                    z += 1;
                    v = (z + 0.5f) * step - 1f;
                }
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, time);
            }
        }

        void UpdateFunctionTransition () {
            FunctionLibrary.Function
                from = FunctionLibrary.GetFunction(transitionFunction),
                to = FunctionLibrary.GetFunction(function);
            float progress = duration / transitionDuration;
            float time = Time.time;
            float step = 2f / resolution;
            float v = 0.5f * step - 1f;
            for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++) {
                if (x == resolution) {
                    x = 0;
                    z += 1;
                    v = (z + 0.5f) * step - 1f;
                }
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = FunctionLibrary.Morph(
                    u, v, time, from, to, progress
                );
            }
        }
    }
}