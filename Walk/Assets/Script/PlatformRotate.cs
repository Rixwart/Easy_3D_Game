using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotate : MonoBehaviour
{

    [SerializeField] float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion rotate = Quaternion.AngleAxis(1*(rotationSpeed*(-10)), new Vector3(0, 1, 0));
        transform.rotation *= rotate;
    }
}
