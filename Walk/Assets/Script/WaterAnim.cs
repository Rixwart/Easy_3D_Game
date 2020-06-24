using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnim : MonoBehaviour
{
    public float move_speed = 0.3f;
    public float upSpeed,move_x, move_y = 0;
    

    private Renderer rend;

    private float offset_x;
    private float offset_y;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        offset_y = Time.time * move_speed* move_x;
        offset_x = Time.time * move_speed* move_y;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset_x,offset_y));
        this.transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * upSpeed), transform.position.z);
    }


}
