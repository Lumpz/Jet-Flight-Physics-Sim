using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tailWingScript : MonoBehaviour
{
    Rigidbody tailBody;
    public float xVel;
    public float yVel;
    public float zVel;
    // Start is called before the first frame update
    void Start()
    {
        tailBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       

    }
}
