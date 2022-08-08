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
		float step = 2f / resolution;
		var scale = Vector3.one * step;

		points = new Transform[resolution * resolution];
		for (int i = 0; i < points.Length; i++) {
			Transform point = points[i] = Instantiate(pointPrefab);
			point.localScale = scale;
			point.SetParent(transform, false);
		}
	}

    void Update () {
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
			//float v = (z + 0.5f) * step - 1f;
			points[i].localPosition = f(u, v, time);
		}
	}
}