using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRocking : MonoBehaviour
{
    private float swingSpeed = 15.0f;
    private float spinAngle = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * swingSpeed * Time.deltaTime, Space.World);

        if (transform.rotation.eulerAngles.z > spinAngle || transform.rotation.eulerAngles.z < -spinAngle)
        {
            swingSpeed = swingSpeed * -1;
            
        }

        

    }
}
