using UnityEngine;

public class GPUGraph5 : MonoBehaviour {

	// Reference for our function library
	[SerializeField]
	ComputeShader computeShader;

	// Adding Properties for our shaders
	static readonly int
		positionsId = Shader.PropertyToID("_Positions"),
		resolutionId = Shader.PropertyToID("_Resolution"),
		stepId = Shader.PropertyToID("_Step"),
		timeId = Shader.PropertyToID("_Time");

	[SerializeField]
	Material material;

	[SerializeField]
	Mesh mesh;


    // Number of points to be created
    [SerializeField, Range(10, 200)]
	int resolution = 10;

    // Which function should we render
	[SerializeField]
	FunctionLibrary5.FunctionName function;
	
	public enum TransitionMode { Cycle, Random }

	[SerializeField]
	TransitionMode transitionMode;

	// How long do we won't to stick with a 
	// particular function 
	[SerializeField, Min(0f)]
	float functionDuration = 1f, transitionDuration = 1f;

	// How long have we been using the current function
	float duration;
	bool transitioning;
	FunctionLibrary5.FunctionName transitionFunction;

	// Buffer for storing information on the GPU it's self,
	// that way we don't have to do transfers between the CPU
	// and the GPU.
	ComputeBuffer positionsBuffer;

	void Awake () {
		//positionsBuffer = new ComputeBuffer();
		positionsBuffer = new ComputeBuffer(resolution * resolution, 3 * 4);
	}
	void OnEnable () {
		positionsBuffer = new ComputeBuffer(resolution * resolution, 3 * 4);
	}

	void OnDisable () {
		positionsBuffer.Release();
		positionsBuffer = null;
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
		}

		UpdateFunctionOnGPU();
	}

	void UpdateFunctionOnGPU () {
		float step = 2f / resolution;
		computeShader.SetInt(resolutionId, resolution);
		computeShader.SetFloat(stepId, step);
		computeShader.SetFloat(timeId, Time.time);
		computeShader.SetBuffer(0, positionsId, positionsBuffer);

		int groups = Mathf.CeilToInt(resolution / 8f);
		computeShader.Dispatch(0, groups, groups, 1);

		// Since the GPU is drawing everything, we won't know the
		// bounds, so go ahead and track that
		var bounds = new Bounds(Vector3.zero, Vector3.one * (2f + 2f / resolution));
		// Have the GPU procedurally generate/draw our objects
		Graphics.DrawMeshInstancedProcedural(
			mesh, 0, material, bounds, positionsBuffer.count
		);
	}

   /**
	* Roll to the next function
	*/
	void PickNextFunction () {
		function = transitionMode == TransitionMode.Cycle ?
			FunctionLibrary5.GetNextFunctionName(function) :
			FunctionLibrary5.GetRandomFunctionNameOtherThan(function);
	}
}