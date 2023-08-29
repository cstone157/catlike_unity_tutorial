using UnityEngine;

using MathematicalSurfaces;

/**
 * NOTE: Skipped section 3.4 - Better Visuals.
 *      I wasn't able to find the shadow setting section, doesn't matter
 *      so I skipped it.
**/
namespace MathematicalSurfaces
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
        
        Transform[] points;

        // On the object awakening
        void Awake () {
            // Initial all of our variables
            float step = 2f / resolution;
            var position = Vector3.zero;
            var scale = Vector3.one * step;
            
            points = new Transform[resolution * resolution];
            for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++) {
                // If the x has reached resolution, wrap around
                if (x == resolution) {
                    x = 0;
                    z += 1;
                }

                // Create the transform for all of our points
                Transform point = points[i] = Instantiate(pointPrefab);

                // Assign the x position (since this never changes this is here, not in the update)
                position.x = (x + 0.5f) * step - 1f;
                position.z = (z + 0.5f) * step - 1f;
                point.localPosition = position;
                point.localScale = scale;

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
            FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);

            float time = Time.time;

            for (int i = 0; i < points.Length; i++) {
                Transform point = points[i];
                Vector3 position = point.localPosition;

                position.y = f(position.x, position.z, time);
                point.localPosition = position;
            }
        }
    }
}