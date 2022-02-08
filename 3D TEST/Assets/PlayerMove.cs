using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{


    CharacterController Controller;

    public float Speed = 20f;
    public float Gravity = -0.1f;

    public Transform Cam;


    // Start is called before the first frame update
    void Start()
    {

        Controller = GetComponent<CharacterController>();
        Speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {

        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = Gravity;



        Controller.Move(Movement);

        if (Movement.magnitude != 0f)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Cam.GetComponent<CameraMove>().sensivity * Time.deltaTime);


            Quaternion CamRotation = Cam.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);

        }
    }

}