using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BodyScript : MonoBehaviour
{

    Rigidbody Body;
    ParticleSystem Afterburn;
    GameObject GroundDustObject;
    ParticleSystem GroundDust;
    AudioSource JetNoise;
    AudioSource WindNoise;
    GameObject landingGear1;
    GameObject landingGear2;
    GameObject landingGear3;
    GameObject LeftGun;
    GameObject RightGun;
    TrailRenderer RightWingTip;
    TrailRenderer LeftWingTip;
    public GameObject bullet;
    public Quaternion startRot;
    public float lift;
   // AudioSource engine;
    Text SpeedText;
    public Camera FollowCam;
    public Camera CockpitCam;
    public Camera StaticFollowCam;
    AudioListener FollowCamListen;
    AudioListener CockpitCamListen;
    AudioListener StaticFollowCamListen;
    public int currentCam = 2;
    public float speed;
    public float acceleration = 0.5f;
    public bool alternateGuns = true;
  //  public float deaceleration = -0.1f;

    // Start is called before the first frame update
    void Start()
    {

        Body = GetComponent<Rigidbody>();
       // engine = GetComponent<AudioSource>();
        Afterburn = GameObject.Find("afterburn").GetComponent<ParticleSystem>();
        GroundDustObject = GameObject.Find("GroundDust");
       GroundDust = GroundDustObject.GetComponent<ParticleSystem>();
        GroundDust.emissionRate = 0f;
        JetNoise = Afterburn.GetComponentInChildren<AudioSource>();
        WindNoise = GetComponent<AudioSource>();
        landingGear1 = GameObject.Find("landingWheel 1");
        landingGear2 = GameObject.Find("landingWheel 2");
        landingGear3 = GameObject.Find("landingWheel 3");
        LeftGun = GameObject.Find("LeftGun");
        RightGun = GameObject.Find("RightGun");
        FollowCam.enabled = true;
        CockpitCam.enabled = false;
        StaticFollowCam.enabled = false;
       CockpitCamListen = CockpitCam.GetComponent<AudioListener>();
        FollowCamListen = FollowCam.GetComponent<AudioListener>();
        StaticFollowCamListen = StaticFollowCam.GetComponent<AudioListener>();
        CockpitCamListen.enabled = false;
        FollowCamListen.enabled = true;
        StaticFollowCamListen.enabled = false;
        SpeedText = FollowCam.GetComponentInChildren<Text>();
        startRot = Body.transform.rotation;

        RightWingTip = GameObject.Find("RightWingTip").GetComponent<TrailRenderer>();
        LeftWingTip = GameObject.Find("LeftWingTip").GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //wings dont let it fall, they are rigid
        float xVel = transform.InverseTransformDirection(Body.velocity).x;
        float yVel = transform.InverseTransformDirection(Body.velocity).y;
        float zVel = transform.InverseTransformDirection(Body.velocity).z;

        if (speed > 175)
        {
            if (zVel < -0f)
                zVel += 7f;
            else if (zVel > 0f)
                zVel -= 7f;

            if (xVel < 0f)
                xVel += 2f;
            else if (xVel > 0f)
                xVel -= 2f;
        }
        else if (speed > 100)
        {
            if (zVel < -0f)
                zVel += 3f;
            else if (zVel > 0f)
                zVel -= 3f;

            if (xVel < 0f)
                xVel += 0.5f;
            else if (xVel > 0f)
                xVel -= 0.5f;
        }
        else if (speed > 50)
        {
            if (zVel < -0f)
                zVel += 1f;
            else if (zVel > 0f)
                zVel -= 1f;

            if (xVel < 0f)
                xVel += 0.2f;
            else if (xVel > 0f)
                xVel -= 0.2f;
        }
        

            //positive xVel moves to the right of the plane
            //positive yVel is backwards
            //positive zVel is up


            var velocity = Body.velocity;
        speed = velocity.magnitude;
        Body.velocity = transform.TransformDirection(new Vector3(xVel, yVel, zVel));


        if (Input.GetKeyDown(KeyCode.G)) //landing gear
        {
            if (landingGear1.active == false)
            {
                landingGear1.SetActive(true);
                landingGear2.SetActive(true);
                landingGear3.SetActive(true);
            }
            else
            {
                landingGear1.SetActive(false);
                landingGear2.SetActive(false);
                landingGear3.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.C)) //switch cam
        {
            switch (currentCam)
            {
                case 1:
                    {
                        FollowCam.enabled = true;
                        CockpitCam.enabled = false;
                        StaticFollowCam.enabled = false;
                        CockpitCamListen.enabled = false;
                        FollowCamListen.enabled = true;
                        StaticFollowCamListen.enabled = false;
                        break;
                    }
                case 2:
                    {
                        FollowCam.enabled = false;
                        CockpitCam.enabled = true;
                        StaticFollowCam.enabled = false;
                        CockpitCamListen.enabled = true;
                        FollowCamListen.enabled = false;
                        StaticFollowCamListen.enabled = false;
                        break;
                    }
                case 3:
                    {
                        FollowCam.enabled = false;
                        CockpitCam.enabled = false;
                        StaticFollowCam.enabled = true;
                        CockpitCamListen.enabled = false;
                        FollowCamListen.enabled = false;
                        StaticFollowCamListen.enabled = true;
                        currentCam = 0;
                        break;
                    }
            }
            currentCam++;
        }

        if (Input.GetKeyDown(KeyCode.R)) //reset plane
        {
            Body.velocity = new Vector3(0f, 0f, 0f);
            this.transform.position = new Vector3(285.15f, 21.05f, -24.93f);
            this.transform.rotation = startRot;
        }


        //update ui stats
        SpeedText.text = "Speed: " + speed.ToString("#0");

        //Wind noise and trails
        if (speed <= 80)
        {
            RightWingTip.startColor = new Color(255, 255, 255, 0f);
            LeftWingTip.startColor = new Color(255, 255, 255, 0f);
            WindNoise.volume = speed / 1000f;
        }
        else
        {
            RightWingTip.startColor = new Color(255, 255, 255, ((speed - 200) / 200)); //convert speed in excess over 100 out of a value of 50  
            LeftWingTip.startColor = new Color(255, 255, 255, ((speed - 200) / 200));
            WindNoise.volume = 0.08f;
        }


        //Draw Ray
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 0; //ground layer

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.

        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, -Vector3.up * hit.distance, Color.yellow);
            GroundDustObject.transform.position = hit.point;
            if (speed > 30 && hit.distance < 10)
            {
                GroundDust.emissionRate = 100f;
            }
            else
            {
                GroundDust.emissionRate = 0f;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, -Vector3.up * 1000, Color.white);
        }

        

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //show afterburn
            Afterburn.Play();
            if (JetNoise.volume < 1.1)
            {
                
                JetNoise.volume += 0.05f;
            }

            if (speed < 250)
            {
                Body.velocity += transform.up * -acceleration;
            }
            else
            {
                Body.velocity += transform.up * -0.1f;
            }

        }
        else if (Input.GetKey(KeyCode.Space) == false)
        {
            //hide afterburn
            Afterburn.Stop();
            if (JetNoise.volume > 0.1)
            {
                JetNoise.volume -= 0.05f;
            }

            if (speed > 5)
            {
                Body.velocity += transform.up * acceleration / 10;
            }
            

        }
        if (Input.GetKey(KeyCode.LeftControl) && -transform.InverseTransformDirection(Body.velocity).y > 10f) //air brake
        {
            Body.velocity += transform.up * 0.5f;
        }

        if (Input.GetMouseButton(0)) //gun
        {
           GameObject firedBullet;
            if (alternateGuns)
            {
                firedBullet = Instantiate(bullet, RightGun.transform.position, RightGun.transform.rotation);
                alternateGuns = !alternateGuns;
            }
            else
            {
                firedBullet = Instantiate(bullet, LeftGun.transform.position, LeftGun.transform.rotation);
                alternateGuns = !alternateGuns;
            }
            firedBullet.GetComponent<Rigidbody>().velocity = transform.up * (-1000f - speed);
            LeftGun.GetComponentInChildren<AudioSource>().mute = false;
        }
        else
        {
            LeftGun.GetComponentInChildren<AudioSource>().mute = true;
        }



        if (Input.GetKey(KeyCode.Q))
        {
            Body.AddTorque(transform.forward * -50f); //yaw right for some reason
        }
        if (Input.GetKey(KeyCode.E))
        {
            Body.AddTorque(transform.forward * 50f); //yaw left for some reason
        }
        if (Input.GetKey(KeyCode.A))
        {
            Body.AddTorque(transform.up * -10f); //roll right for some reason
        }
        if (Input.GetKey(KeyCode.D))
        {
            Body.AddTorque(transform.up * 10f); //roll left for some reason
        }
        if (Input.GetKey(KeyCode.W))
        {
            Body.AddTorque(transform.right * 50f); //pitch down for some reason
        }
        if (Input.GetKey(KeyCode.S))
        {
            Body.AddTorque(transform.right * -50f); //pitch up for some reason
        }

        //lift 
        if (speed < 100f)
        {
            lift = speed / 400f;
        }
        Body.velocity += transform.forward * lift;
    }



}
