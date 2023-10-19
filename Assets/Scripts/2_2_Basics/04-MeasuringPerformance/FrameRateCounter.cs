using UnityEngine;
using TMPro;

namespace MeasuringPerformance
{
    public class FrameRateCounter : MonoBehaviour {

        [SerializeField]
        TextMeshProUGUI display;


        public enum DisplayMode { FPS, MS }

        [SerializeField]
        DisplayMode displayMode = DisplayMode.FPS;

        [SerializeField, Range(0.1f, 2f)]
        float sampleDuration = 1f;

        int frames;
        //float duration;
        float duration, bestDuration = float.MaxValue, worstDuration;

        void Update () {
            float frameDuration = Time.unscaledDeltaTime;
            //display.SetText("FPS\n000\n000\n000");
            //display.SetText("FPS\n{0}\n000\n000", frameDuration);
            //display.SetText("FPS\n{0}\n000\n000", 1f / frameDuration);
            //display.SetText("FPS\n{0:0}\n000\n000", 1f / frameDuration);
        
            frames += 1;
            duration += frameDuration;
            //display.SetText("FPS\n{0:0}\n000\n000", frames / duration);

            if (frameDuration < bestDuration) {
                bestDuration = frameDuration;
            }
            if (frameDuration > worstDuration) {
                worstDuration = frameDuration;
            }

            /*if (duration >= sampleDuration) {
                //display.SetText("FPS\n{0:0}\n000\n000", frames / duration);
                //frames = 0;
                //duration = 0f;

                display.SetText(
                    "FPS\n{0:0}\n{1:0}\n{2:0}",
                    1f / bestDuration,
                    frames / duration,
                    1f / worstDuration
                );
                frames = 0;
                duration = 0f;
                bestDuration = float.MaxValue;
                worstDuration = 0f;            
            }*/

            if (duration >= sampleDuration) {
                if (displayMode == DisplayMode.FPS) {
                    display.SetText(
                        "FPS\n{0:0}\n{1:0}\n{2:0}",
                        1f / bestDuration,
                        frames / duration,
                        1f / worstDuration
                    );
                }
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
}