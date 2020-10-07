using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class upright : MonoBehaviour
{
    Rigidbody rBody;
    float steer;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        steer = Input.GetAxis("Horizontal");

        if (Math.Abs(steer) > .01)
            rBody.constraints = RigidbodyConstraints.FreezeRotationZ;
        else
            rBody.constraints = RigidbodyConstraints.None;


    }
}
