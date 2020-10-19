using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carEffects : MonoBehaviour
{
    public Light brakeL;
    public Light brakeR;

    public AudioSource wheelFL;
    public AudioSource wheelFR;
    public AudioSource wheelRL;
    public AudioSource wheelRR;
    public AudioSource engine;

    public AudioClip accelerate;
    public AudioClip deccelerate;

    public GameObject skid;
    [SerializeField] Transform[] skidPlaces = new Transform[4];
    [SerializeField] ParticleSystem[] particles = new ParticleSystem[4];

    private Rigidbody rb;
    private bool skidding = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        engineSound();
        skidStart();

        if (Input.GetKey(KeyCode.Space))
        {
            brakeL.intensity = 1;
            brakeR.intensity = 1;

            if (rb.velocity.magnitude > 0.1f)
            {
                //smokeStart();
                skidding = true;

                if (!wheelFL.isPlaying)
                {
                    wheelFL.Play();
                    wheelFR.Play();
                    wheelRL.Play();
                    wheelRR.Play();
                }
                
            }
            else
            {
                //smokeStop();
                skidding = false;

                wheelFL.Stop();
                wheelFR.Stop();
                wheelRL.Stop();
                wheelRR.Stop();
            }
        }
        else
        {
            //smokeStop();
            skidding = false;

            brakeL.intensity = 0;
            brakeR.intensity = 0;

            wheelFL.Stop();
            wheelFR.Stop();
            wheelRL.Stop();
            wheelRR.Stop();
        }
    }

    private void engineSound()
    {
        //Debug.Log(rb.velocity.magnitude);

        if (rb.velocity.magnitude > 0.1f)
        {
            if (!engine.isPlaying)
            {
                engine.Play();
            }

            if (Input.GetAxis("Vertical") != 0)
            {
                if (engine.pitch < 2.1f)
                {
                    engine.pitch += (rb.velocity.magnitude * .001f);
                }
                engine.clip = accelerate;
            }
            else
            {
                if (engine.pitch > 1)
                {
                    engine.pitch -= (rb.velocity.magnitude * .001f);
                }
                //engine.clip = deccelerate;
            }
        }
        else
        {
            engine.pitch = 1;
            engine.Stop();
        }
    }

    private void skidStart()
    {
        if (skidding)
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(skid, skidPlaces[i].position, Quaternion.identity);
            }
        }
    }

    private void smokeStart()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!particles[i].isPlaying)
            {
                particles[i].Play();
            }
        }
    }

    private void smokeStop()
    {
        for (int i = 0; i < 4; i++)
        {
                particles[i].Stop();
        }
    }
}
