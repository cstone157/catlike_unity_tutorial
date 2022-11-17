using UnityEngine;

public class Graph : MonoBehaviour {

	[SerializeField]
	Transform pointPrefab;

    // Number of points to be created
    [SerializeField, Range(10, 100)]
	int resolution = 10;

    // The points that we are graphing  
    Transform[] points;
    void Awake () {
        // Scale to stay within the 1 by 1 domain
        float step = 2f / resolution;

		var position = Vector3.zero;
        var scale = Vector3.one * step;

        // Create an array of points, for storing
        points = new Transform[resolution];

        for (int i = 0; i < points.Length; i++) {
            Transform point = points[i] = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
			point.localPosition = position;
			point.localScale = scale;
            point.SetParent(transform, false);
		}
    }

    void Update () {
        float time = Time.time;

        // Loop trough our points and update their positions
		for (int i = 0; i < points.Length; i++) {
            Transform point = points[i];
			Vector3 position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));
            //position.y = Mathf.Sin(Mathf.PI * (position.x));
			point.localPosition = position;
        }
	}
}