using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Control : MonoBehaviour
{
    private float hAxis;
    private float vAxis;
    private bool brakeing;
    private float currentBrakeForce;
    private float steerAngle;
    private Rigidbody rBody;

    [SerializeField] private float horsePower;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider FL_collider;
    [SerializeField] private WheelCollider FR_collider;
    [SerializeField] private WheelCollider RL_collider;
    [SerializeField] private WheelCollider RR_collider;

    [SerializeField] private Transform FL_transform;
    [SerializeField] private Transform FR_transform;
    [SerializeField] private Transform RL_transform;
    [SerializeField] private Transform RR_transform;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetInput();
        Motor();
        Steering();
        //UpdateWheels();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = -Input.GetAxis("Vertical");
        brakeing = Input.GetKey(KeyCode.Space);
    }

    private void Motor()
    {
        FL_collider.motorTorque = -vAxis * horsePower;
        FR_collider.motorTorque = -vAxis * horsePower;
        RL_collider.motorTorque = -vAxis * horsePower;
        RR_collider.motorTorque = -vAxis * horsePower;
        currentBrakeForce = brakeing ? brakeForce : 0f;

        Brake();
    }

    private void Brake()
    {
        FL_collider.brakeTorque = currentBrakeForce;
        FR_collider.brakeTorque = currentBrakeForce;
        RL_collider.brakeTorque = currentBrakeForce;
        RR_collider.brakeTorque = currentBrakeForce;
    }

    private void Steering()
    {
        steerAngle = maxSteerAngle * hAxis;

        FL_collider.steerAngle = steerAngle;
        FR_collider.steerAngle = steerAngle;
    }

    private void UpdateWheels()
    {
        SingleWheel(FR_collider, FR_transform);
        SingleWheel(FL_collider, FL_transform);
        SingleWheel(RR_collider, RR_transform);
        SingleWheel(RL_collider, RL_transform);
    }

    private void SingleWheel(WheelCollider col, Transform trans)
    {
        Vector3 pos;
        Quaternion rotate;

        col.GetWorldPose(out pos, out rotate);
        trans.position = pos;
        trans.rotation = rotate;
    }

}
