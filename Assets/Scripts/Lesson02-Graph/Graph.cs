using UnityEngine;

public class Graph : MonoBehaviour {

	[SerializeField]
	Transform pointPrefab;

    // Number of points to be created
    [SerializeField, Range(10, 100)]
	int resolution = 10;

    void Awake () {
        // Create an instance of a prefab
//		Instantiate(pointPrefab);

        // Create an instance of a prefab and move to the right
//        Transform point = Instantiate(pointPrefab);
//        point.localPosition = Vector3.right;

        //Transform point = Instantiate(pointPrefab);
		//point = Instantiate(pointPrefab);
        //point.localPosition = Vector3.right * 2f;   

        //Loop across i and create a series of points
/*        int i = 0;
		while (i < 10) {
			//Transform point = Instantiate(pointPrefab);
			//point.localPosition = Vector3.right;

            Transform point = Instantiate(pointPrefab);
			point.localPosition = Vector3.right * i;
			//i = i + 1;
		}
*/
        // Scale to stay within the 1 by 1 domain
        float step = 2f / resolution;

        //Vector3 position;
		var position = Vector3.zero;
//		var scale = Vector3.one / 5f;
        var scale = Vector3.one * step;

//		for (int i = 0; i < 10; i++) {
        for (int i = 0; i < resolution; i++) {
			Transform point = Instantiate(pointPrefab);
//			point.localPosition = Vector3.right * ((i + 0.5f) / 5f - 1f);
			
            // Straight line of points
//            position.x = (i + 0.5f) / 5f - 1f;
//            position.y = position.x;

            // Parabola line of points
//            position.x = (i + 0.5f) / 5f - 1f;
            position.x = (i + 0.5f) * step - 1f;
            position.y = position.x * position.x;

			point.localPosition = position;
			point.localScale = scale;

            point.SetParent(transform, false);
		}
    }
}