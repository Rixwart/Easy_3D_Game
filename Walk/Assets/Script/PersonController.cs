using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class PersonController : MonoBehaviour
{
    [SerializeField] public GameObject camera;
    Transform checkpoint;
    [SerializeField] public GameObject LosePanel;
    [SerializeField] public GameObject WinScreen;
    bool canActive = false;
    bool IsPaused = false;
    private CharacterController control;
    Quaternion StartRotation;

    float Ver, Hor, Jump, RotHor, RotVer;
    [SerializeField] float Speed = 1f, JumpSpeed=1f, sensivity=10f, moveBoost, boost=1;

    bool IsGround = false;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.visible = false;
        StartRotation = transform.rotation;
        Time.timeScale = 1f;
        control = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale == 0f) { IsPaused = true; Cursor.visible = true; } else { IsPaused = false; Cursor.visible = false;}
        if (!IsPaused)
        {
            RotHor += Input.GetAxis("Mouse X") * sensivity;
            RotVer += Input.GetAxis("Mouse Y") * sensivity;

            RotVer = Mathf.Clamp(RotVer, -60, 60);

            Quaternion RotX = Quaternion.AngleAxis(RotHor, Vector3.up);
            Quaternion RotY = Quaternion.AngleAxis(-RotVer, Vector3.right);

            camera.transform.rotation = StartRotation * transform.rotation * RotY;
            transform.rotation = StartRotation * RotX;
        }
    }
    
    void FixedUpdate()
    {

        if (IsGround)
        {
            if (Input.GetKey(KeyCode.LeftShift)) boost = moveBoost; else boost = 1;
            Ver = Input.GetAxis("Vertical") * Time.deltaTime * Speed*boost;
            Hor = Input.GetAxis("Horizontal") * Time.deltaTime * Speed * boost;

            Jump = Input.GetAxis("Jump") * Time.deltaTime * JumpSpeed;          
            GetComponent<Rigidbody>().AddForce(transform.up * Jump, ForceMode.Impulse);
        }
        transform.Translate(new Vector3(Hor,0,Ver));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("platform_one"))
        {
            this.transform.parent = collision.transform;
        }
        if (collision.gameObject.name.Equals("point1"))
        {
            IsPaused = false;
        }
        if (collision.gameObject.tag == "death")
        {
            Time.timeScale = 0f;
            IsPaused = true;
            LosePanel.SetActive(true);         
        }
    }

    void GetCheckpoint()
    {
        transform.position = checkpoint.position;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGround = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGround = false;
        }
        if (collision.gameObject.name.Equals("platform_one"))
        {
            this.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0f;
        IsPaused = true;
        WinScreen.SetActive(true);
    }

}
