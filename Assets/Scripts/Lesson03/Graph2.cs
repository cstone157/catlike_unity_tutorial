using UnityEngine;

public class Graph2 : MonoBehaviour {

	[SerializeField]
	Transform pointPrefab;

    // Number of points to be created
    [SerializeField, Range(10, 100)]
	int resolution = 10;

    // Which function should we render
	[SerializeField]
	FunctionLibrary.FunctionName function;


    // The points that we are graphing  
    Transform[] points;
    void Awake () {
        // Scale to stay within the 1 by 1 domain
        float step = 2f / resolution;

		var position = Vector3.zero;
        var scale = Vector3.one * step;

        // Create an array of points, for storing
        points = new Transform[resolution * resolution];

        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++) {
            if (x == resolution) {
				x = 0;
                z += 1;
			}

            Transform point = points[i] = Instantiate(pointPrefab);
            position.x = (x + 0.5f) * step - 1f;
            position.z = (z + 0.5f) * step - 1f;
            
			point.localPosition = position;
			point.localScale = scale;
            point.SetParent(transform, false);
		}
    }

    void Update () {
        float time = Time.time;
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);

        // Loop trough our points and update their positions
		for (int i = 0; i < points.Length; i++) {
            Transform point = points[i];
			Vector3 position = point.localPosition;
            position.y = f(position.x, position.z, time);
			point.localPosition = position;
        }
	}
}