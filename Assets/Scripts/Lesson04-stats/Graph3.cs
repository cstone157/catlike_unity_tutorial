using UnityEngine;

public class Graph3 : MonoBehaviour {

	[SerializeField]
	Transform pointPrefab;

    // Number of points to be created
    [SerializeField, Range(10, 100)]
	int resolution = 10;

    // Which function should we render
	[SerializeField]
	FunctionLibrary3.FunctionName function;
	
	public enum TransitionMode { Cycle, Random }

	[SerializeField]
	TransitionMode transitionMode;

	// How long do we won't to stick with a 
	// particular function 
	[SerializeField, Min(0f)]
	float functionDuration = 1f, transitionDuration = 1f;

    // The points that we are graphing  
    Transform[] points;

	// How long have we been using the current function
	float duration;
	bool transitioning;
	FunctionLibrary3.FunctionName transitionFunction;


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
		duration += Time.deltaTime;
		if (duration >= functionDuration) {
			if (transitioning) {
				if (duration >= transitionDuration) {
					duration -= transitionDuration;
					transitioning = false;
				}
			}
			else if (duration >= functionDuration) {
				duration -= functionDuration;
				transitioning = true;
				transitionFunction = function;
				PickNextFunction();
			}

			// Since we will likely over shoot, so don't
			// count that against ourself
			//duration -= functionDuration;
			//function = FunctionLibrary.GetNextFunctionName(function);

			//transitioning = true;
			//transitionFunction = function;
			//PickNextFunction();
		}

		if (transitioning) {
			UpdateFunctionTransition();
		}
		else {
			UpdateFunction();
		}
	}

   /**
	* Roll to the next function
	*/
	void PickNextFunction () {
		function = transitionMode == TransitionMode.Cycle ?
			FunctionLibrary3.GetNextFunctionName(function) :
			FunctionLibrary3.GetRandomFunctionNameOtherThan(function);
	}

	// Track how long we have been in the current function and update
	// once the time goes by
	void UpdateFunction () {
		FunctionLibrary3.Function f = FunctionLibrary3.GetFunction(function);
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
		FunctionLibrary3.Function from = FunctionLibrary3.GetFunction(transitionFunction),
			to = FunctionLibrary3.GetFunction(function);

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
			points[i].localPosition = FunctionLibrary3.Morph(
				u, v, time, from, to, progress
			);
		}
	}

}