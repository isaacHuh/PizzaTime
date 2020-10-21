using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PizzaSelectCamera : MonoBehaviour
{
    public GameObject topDown;
    public GameObject ogView;
    

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            switchCamera();
        }

    }

    void switchCamera()
    {
        mainCamera.transform.position = topDown.transform.position;
        mainCamera.transform.rotation = topDown.transform.rotation;
    }

    void switchCameraBack()
    {
        mainCamera.transform.position = ogTransform;
        mainCamera.transform.rotation = ogRotation;
    }

}