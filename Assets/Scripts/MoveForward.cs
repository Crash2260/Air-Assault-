using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Vector3 initPos;
    public float speed = 40.0f;
    public float zDestroy = 20.0f;
    private float bulletDistance = 20.0f;
    // Start is called before the first frame update
    void Awake()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(transform.position.z > initPos.z + bulletDistance)
        {
            Destroy(gameObject);
        }
    }
}