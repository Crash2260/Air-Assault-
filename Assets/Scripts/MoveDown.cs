using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveDown : MonoBehaviour
{
    public float speed = 2.5f;
    //private float zDestroy = -18.0f;
    private float repeatInterval = 103;

    private Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        //get initial position of the ground
        if (CompareTag("Ground") || CompareTag("Wall")) ;
        {

            startingPos = transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);


        if (CompareTag("Enviroment")) 
        {
            if (transform.position.z < startingPos.z - repeatInterval)
            {
                transform.position = startingPos;

            }
        }
    }
}
