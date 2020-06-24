using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMove : MonoBehaviour
{
    [SerializeField] GameObject camera;
    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = camera.transform.position;
        
    }
}
