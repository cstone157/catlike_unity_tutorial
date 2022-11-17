using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDelta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)){
            
            //transform.Translate(new Vector3(0f, .5f, 0f));
            // Since this function runs per update, .5f we are running at
            //    at 40 fps, then the result would be
            //transform.Translate(40 * new Vector3(0f, .5f, 0f));
            // but if we run at 100 fps then it would be
            //transform.Translate(100 * new Vector3(0f, .5f, 0f));

            // So to ensure that our object moves at a constant speed then we
            //    use :
            transform.Translate(100 * new Vector3(0f, .5f, 0f) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(100 * new Vector3(0f, -.5f, 0f) * Time.deltaTime);
        }
    }
}
