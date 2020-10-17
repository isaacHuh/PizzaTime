using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PizzaSelectCamera : MonoBehaviour
{
    public Camera topDownCamera;
    public Camera mainCamera;

    AudioListener mainCameraListener;
    AudioListener topCameraListener;

    void Start()
    {
        mainCamera.enabled = true;
        topDownCamera.enabled = false;
        mainCameraListener = mainCamera.GetComponent<AudioListener>();
        topCameraListener = topDownCamera.GetComponent<AudioListener>();
        topCameraListener.enabled = false;
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            switchCamera();
        }

    }
    void OnTriggerExit(Collider other)
    {
        switchCameraBack();
    }

    void switchCamera()
    {
        topDownCamera.enabled = true;        
        topCameraListener.enabled = true;
        mainCameraListener.enabled = false;
        mainCamera.enabled = false;
    }

    void switchCameraBack()
    {

        mainCameraListener.enabled = true;
        mainCamera.enabled = true;
        topCameraListener.enabled = false;
        topDownCamera.enabled = false;

    }

}