using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 10.0f;
    private float zDestroyTop = -20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * bulletSpeed * Time.deltaTime);
        //Destroy bullet at bottom of screen
        if(transform.position.z < zDestroyTop)
        {
            Destroy(gameObject);
        }
    }
}
