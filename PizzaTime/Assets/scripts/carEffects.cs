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

    private Rigidbody rb;
    private float currentSpeed = 0;
    private float lastSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastSpeed = currentSpeed;
        currentSpeed = rb.velocity.magnitude;

        //engineSound(lastSpeed, currentSpeed);

        if (Input.GetKey(KeyCode.Space))
        {
            brakeL.intensity = 1;
            brakeR.intensity = 1;

            if (rb.velocity.magnitude > 0.1f)
            {
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
                wheelFL.Stop();
                wheelFR.Stop();
                wheelRL.Stop();
                wheelRR.Stop();
            }
        }
        else
        {
            brakeL.intensity = 0;
            brakeR.intensity = 0;

            wheelFL.Stop();
            wheelFR.Stop();
            wheelRL.Stop();
            wheelRR.Stop();
        }
    }

    private void engineSound(float last, float current)
    {
        if (current > last)
        {
            engine.clip = accelerate;

            if (!engine.isPlaying)
            {
                engine.Play();
            }
        }
    }
}
