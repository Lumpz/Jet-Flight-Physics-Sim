using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{

    public GameObject Jet;
    public Rigidbody JetBody;
    public Quaternion startRot;
    public Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        JetBody = Jet.GetComponent<Rigidbody>();
        startRot = Jet.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        body.transform.position = new Vector3(Jet.transform.position.x, Jet.transform.position.y + 1.5f,Jet.transform.position.z);
        body.transform.rotation = Jet.transform.rotation;
    }


    void OnCollisionEnter(Collision collision)
    {
        Rigidbody CollisionBody = collision.rigidbody;
        if (collision.gameObject.tag != "Player")
        {
            Respawn();

        }


    }

    public void Respawn()
    {
        JetBody.velocity = new Vector3(0f,0f,0f);
        Jet.transform.position = new Vector3(285.15f, 21.05f, -24.93f);
        Jet.transform.rotation = startRot;
    }

}
