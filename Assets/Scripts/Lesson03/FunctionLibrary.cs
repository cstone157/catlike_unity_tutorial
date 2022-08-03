using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary {
	public delegate float Function (float x, float t);
	static Function[] functions = { Wave, MultiWave, Ripple };

	public static Function GetFunction (int index) {
		return functions[index];
	}

  /**
    * Create a wave for our points
    */
  public static float Wave (float x, float t) {
		return Sin(PI * (x + t));
	}

  /**
    * Create a multi wave for our points
    */
	public static float MultiWave (float x, float t) {
//		float y = Sin(PI * (x + t));
    float y = Sin(PI * (x + 0.5f * t));
		y += 0.5f * Sin(2f * PI * (x + t));
		return y * (2f / 3f);
	}

  /**
    * Create a ripple for our points
    */
  public static float Ripple (float x, float t) {
		float d = Abs(x);
		float y = Sin(PI * (4f * d - t));
		return y / (1f + 10f * d);
	}
}