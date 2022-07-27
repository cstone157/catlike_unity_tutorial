using UnityEngine;

public class Graph : MonoBehaviour {

	[SerializeField]
	Transform pointPrefab;

    void Awake () {
		//Instantiate(pointPrefab);
        //Transform point = Instantiate(pointPrefab);
        //point.localPosition = Vector3.right;

        //Transform point = Instantiate(pointPrefab);
		//point = Instantiate(pointPrefab);
        //point.localPosition = Vector3.right * 2f;   

        //while (false) {
        int i = 0;
		while (i < 10) {
			//Transform point = Instantiate(pointPrefab);
			//point.localPosition = Vector3.right;

            Transform point = Instantiate(pointPrefab);
			point.localPosition = Vector3.right * i;
			i = i + 1;
		}
    }
}