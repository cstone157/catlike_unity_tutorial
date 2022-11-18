using UnityEngine;
using TMPro;

public class FrameRateCounter5 : MonoBehaviour {

	[SerializeField]
	TextMeshProUGUI display;
	
	public enum DisplayMode { FPS, MS }

	[SerializeField]
	DisplayMode displayMode = DisplayMode.FPS;

	[SerializeField, Range(0.1f, 2f)]
	float sampleDuration = 1f;

    int frames;
	float duration, bestDuration = float.MaxValue, worstDuration;
    
    void Update () {
		float frameDuration = Time.unscaledDeltaTime;
		frames += 1;
		duration += frameDuration;

        if (frameDuration < bestDuration) {
			bestDuration = frameDuration;
		}
		if (frameDuration > worstDuration) {
			worstDuration = frameDuration;
		}

		if (duration >= sampleDuration) {
			// FPS isn't a great mode for calculating our FPS
			if (displayMode == DisplayMode.FPS) {
				display.SetText(
					"FPS\n{0:0}\n{1:0}\n{2:0}",
					1f / bestDuration,
					frames / duration,
					1f / worstDuration
				);
			} // More useful is the length of our FPS
			else {
				display.SetText(
					"MS\n{0:1}\n{1:1}\n{2:1}",
					1000f * bestDuration,
					1000f * duration / frames,
					1000f * worstDuration
				);
			}
			frames = 0;
			duration = 0f;
			bestDuration = float.MaxValue;
			worstDuration = 0f;
		}
	}
}