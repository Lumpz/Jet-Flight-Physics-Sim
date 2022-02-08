using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wingScript : MonoBehaviour
{

    Rigidbody wingBody;
    public float lift;
    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        wingBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       var velocity = wingBody.velocity;
        speed = velocity.magnitude;


    }

    private void FixedUpdate()
    {
        
        lift = speed / 10;
        //if (lift > 0.25f)
        //    lift = 0.25f;

        //push up ast a constant rate
       // wingBody.velocity += transform.up * lift;

    }
}
