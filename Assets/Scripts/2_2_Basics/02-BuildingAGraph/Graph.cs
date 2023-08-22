using UnityEngine;

public class Graph : MonoBehaviour {

    // The prefab of our point object
	[SerializeField]
	Transform pointPrefab;

    // Number of cubes to create
	//[SerializeField]
    //[SerializeField, Range(10, 100)]
    [SerializeField, Range(10, 100)]
	int resolution = 10;

    //Transform points;
    Transform[] points;

    // On the object awakening
    void Awake () {
        /*
        // Instantiate: creates a copy of the object that is passed
		// Instantiate(pointPrefab);
        Transform point = Instantiate(pointPrefab);
        // Shift our point to the right
        point.localPosition = Vector3.right;

        // Create a second point and shift even further to the right
		point = Instantiate(pointPrefab);
        point.localPosition = Vector3.right * 2f;
        */

        /*
        // Loop to create all of our Points
        //int i = 0;
		//while (i < 10) {
		//	i = i + 1;
		for (int i = 0; i < 10; i++) {
        	Transform point = Instantiate(pointPrefab);
			point.localPosition = Vector3.right * i;
            // Change the scale of our point
            point.localPosition = Vector3.right * ((i + 0.5f) / 5f - 1f);
		}
        */

        float step = 2f / resolution;
        var position = Vector3.zero;
		//var scale = Vector3.one / 5f;
		var scale = Vector3.one * step;
		//for (int i = 0; i < 10; i++) {
        
        points = new Transform[resolution];
		for (int i = 0; i < points.Length; i++) {
        	//Transform point = Instantiate(pointPrefab);
            //points[i] = point;
			Transform point = points[i] = Instantiate(pointPrefab);

            //position.x = (i + 0.5f) / 5f - 1f;
			position.x = (i + 0.5f) * step - 1f;
            //position.y = position.x;
            //position.y = position.x * position.x;
			//position.y = position.x * position.x * position.x;
            point.localPosition = position;
			point.localScale = scale;

            //point.SetParent(transform);
            // When a new parent is set Unity will attempt to keep 
            //  the object at its original world position, rotation, 
            //  and scale. We don't need this in our case. We can 
            //  signal this by passing false as a second argument 
            //  to SetParent.
            point.SetParent(transform, false);
		}
    }

    void Update () {
        float time = Time.time;

		for (int i = 0; i < points.Length; i++) {
			Transform point = points[i];
			Vector3 position = point.localPosition;
            
            //position.y = position.x * position.x * position.x;
            //position.y = Mathf.Sin(position.x);
            //position.y = Mathf.Sin(Mathf.PI * position.x);
            //position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));
            
            point.localPosition = position;
		}
	}
}