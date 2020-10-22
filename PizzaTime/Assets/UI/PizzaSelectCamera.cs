using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PizzaSelectCamera : MonoBehaviour
{
    public GameObject topDown;
    public GameObject ogView;
    public Camera mainCamera;
    

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            switchCamera();
        }
        if (collider.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Q))
        {
            switchCameraBack();
            gameObject.SetActive(false);
            collider.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        gameObject.SetActive(true);
    }

    void switchCamera()
    {
        mainCamera.transform.position = topDown.transform.position;
        mainCamera.transform.rotation = topDown.transform.rotation;
    }

    void switchCameraBack()
    {
        mainCamera.transform.position = ogView.transform.position;
        mainCamera.transform.rotation = ogView.transform.rotation;
    }

}