using System;
using UnityEngine;

public class Clock : MonoBehaviour {
    [SerializeField]
    Transform hoursPivot, minutesPivot, secondsPivot;

    //float hoursToDegrees = -30f;
    const float hoursToDegrees = -30f, minutesToDegrees = -6f, secondsToDegrees = -6f;

    //void Awake () {
    void Update () {
        //DateTime time = DateTime.Now;
        TimeSpan time = DateTime.Now.TimeOfDay;

		//Debug.Log(DateTime.Now.Hour);
		hoursPivot.localRotation = //Quaternion.Euler(0, 0, -30 * DateTime.Now.Hour);
            //Quaternion.Euler(0, 0, hoursToDegrees * DateTime.Now.Hour);
            //Quaternion.Euler(0, 0, hoursToDegrees * time.Hour);
            Quaternion.Euler(0, 0, hoursToDegrees * (float)time.TotalHours);
        minutesPivot.localRotation =
			Quaternion.Euler(0f, 0f, minutesToDegrees * (float)time.TotalMinutes);
		secondsPivot.localRotation =
			Quaternion.Euler(0f, 0f, secondsToDegrees * (float)time.TotalSeconds);
	}
}