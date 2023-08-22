using UnityEngine;

namespace MathematicalSurfaces
{
    public class Graph : MonoBehaviour {

        // The prefab of our point object
        [SerializeField]
        Transform pointPrefab;

        // Number of cubes to create
        [SerializeField, Range(10, 100)]
        int resolution = 10;

        Transform[] points;

        // On the object awakening
        void Awake () {
            // Initial all of our variables
            float step = 2f / resolution;
            var position = Vector3.zero;
            var scale = Vector3.one * step;
            
            points = new Transform[resolution];
            for (int i = 0; i < points.Length; i++) {
                // Create the transform for all of our points
                Transform point = points[i] = Instantiate(pointPrefab);

                // Assign the x position (since this never changes this is here, not in the update)
                position.x = (i + 0.5f) * step - 1f;
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
            float time = Time.time;

            for (int i = 0; i < points.Length; i++) {
                Transform point = points[i];
                Vector3 position = point.localPosition;
                
                // Generate a sine wave, based upon time
                position.y = Mathf.Sin(Mathf.PI * (position.x + time));

                point.localPosition = position;
            }
        }
    }
}