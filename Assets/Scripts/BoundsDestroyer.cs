using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDestroyer : MonoBehaviour
{
    private float lowerBound = -25.0f;
    private float upperBound = 55.0f;
    private float sideBounds = 17.5f;
    private float skyLimit = 6.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > upperBound || transform.position.z < lowerBound)
        {
            Destroy(gameObject);
        }

        if (transform.position.x > sideBounds || transform.position.x < -sideBounds)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < 0 || transform.position.y > skyLimit)
        {
            Destroy(gameObject);
        }
    }
}
