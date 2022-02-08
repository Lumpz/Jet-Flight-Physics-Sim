using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public int lifeTimer = 0;
    public GameObject poof;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer++;
        if (lifeTimer > 3000)
        {
            Object.Destroy(this.gameObject);
        }
    }


    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            poof.transform.parent = null;
            poof.GetComponent<ParticleSystem>().Play();
            Object.Destroy(this.gameObject);
        }

    }

}

