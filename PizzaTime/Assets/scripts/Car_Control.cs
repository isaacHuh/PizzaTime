using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Control : MonoBehaviour
{
    private float hAxis; //Forward/Back input float
    private float vAxis; // Left/Right input float
    private bool brakeing; // Space input bool
    private float currentBrakeForce; // Brakeforce being applied currently
    private float steerAngle; // Direction applied to wheels
    private float currentSteerAngle; // Current angle of wheels
    private Rigidbody rBody; // Car Rigidbody
    private WheelFrictionCurve friction; // Current sideways friction of wheels
    private Vector3 spawn; // Position to return car to at respawn
    private Quaternion rotation; // Rotation to return car to at respawn

    [SerializeField] private float horsePower; // Strength of forward motion
    [SerializeField] private float brakeForce; // Strength of breaking
    [SerializeField] private float maxSteerAngle; // Maximum angle allowed for turning

    [SerializeField] private WheelCollider FL_collider; //Wheel Colliders
    [SerializeField] private WheelCollider FR_collider;
    [SerializeField] private WheelCollider RL_collider;
    [SerializeField] private WheelCollider RR_collider;

    [SerializeField] private Transform FL_transform; //Wheel Transforms
    [SerializeField] private Transform FR_transform;
    [SerializeField] private Transform RL_transform;
    [SerializeField] private Transform RR_transform;

    void Start()
    {
        rBody = GetComponent<Rigidbody>(); // Get Rigidbody
        friction = FL_collider.sidewaysFriction; // Get starting friction of tires
        spawn = transform.position; // Remember starting position and rotation
        rotation = transform.rotation;
        
    }

    private void FixedUpdate()
    {
        GetInput(); // Monitor player input
        Motor(); // Simulate engine
        Steering(); // Position tires
        Respawn();
        //UpdateWheels();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = -Input.GetAxis("Vertical");
        brakeing = Input.GetKey(KeyCode.Space);
    }

    private void Motor() // Assign torque to wheels
    {
        FL_collider.motorTorque = -vAxis * horsePower;
        FR_collider.motorTorque = -vAxis * horsePower;
        RL_collider.motorTorque = -vAxis * horsePower;
        RR_collider.motorTorque = -vAxis * horsePower;
        

        if (brakeing) // If player is brakeing...
        {
            currentBrakeForce = brakeForce;  // Apply brakeforce
            currentSteerAngle = 90f; // Increase steering angle (to assist drifting)

            if (hAxis > .01f) // While player is accelerating and brakeing...
                DriftOn(); // Reduce friction
            else
                DriftOff();
        }
        else // Return all variables to standard when not brakeing
        {
            currentBrakeForce = 0f;
            currentSteerAngle = maxSteerAngle;
            DriftOff();
        }

        Brake();
    }

    private void Brake() // Apply brake torque to wheels
    {
        FL_collider.brakeTorque = currentBrakeForce;
        FR_collider.brakeTorque = currentBrakeForce;
        RL_collider.brakeTorque = currentBrakeForce;
        RR_collider.brakeTorque = currentBrakeForce;
    }

    private void Steering() // Turn front wheels
    {
        steerAngle = currentSteerAngle * hAxis;

        FL_collider.steerAngle = steerAngle;
        FR_collider.steerAngle = steerAngle;
    }

    private void UpdateWheels() // Currently non-operable
    {
        SingleWheel(FR_collider, FR_transform);
        SingleWheel(FL_collider, FL_transform);
        SingleWheel(RR_collider, RR_transform);
        SingleWheel(RL_collider, RL_transform);
    }

    private void SingleWheel(WheelCollider col, Transform trans) // Currently non-operable
    {
        Vector3 pos;
        Quaternion rotate;

        col.GetWorldPose(out pos, out rotate);
        trans.position = pos;
        trans.rotation = rotate;
    }

    private void DriftOn() // Reduce sideways friction to all wheels
    {
        friction.stiffness = 0f;

        FL_collider.sidewaysFriction = friction;
        FR_collider.sidewaysFriction = friction;
        RL_collider.sidewaysFriction = friction;
        RR_collider.sidewaysFriction = friction;
    }

    private void DriftOff() // Return sideways friction to normal
    {
        friction.stiffness = 1;

        FL_collider.sidewaysFriction = friction;
        FR_collider.sidewaysFriction = friction;
        RL_collider.sidewaysFriction = friction;
        RR_collider.sidewaysFriction = friction;
    }

    private void Respawn() // Return car to starting point
    {

        if (Input.GetKey(KeyCode.R))
        {
            transform.position = spawn;
            transform.rotation = rotation;
        }
    }

}
