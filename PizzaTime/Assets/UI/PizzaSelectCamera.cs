using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

public class PizzaSelectCamera : MonoBehaviour
{
    public GameObject topDown;
    public GameObject ogView;
    public Camera mainCamera;
    public TMP_Text pressE;
    public TMP_Text pressQ;


    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            pressE.text = "";
            pressQ.text = "Press Q to quit";
            switchCamera();
            
        }
        if (collider.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Q))
        {
            switchCameraBack();
            pressQ.text = "";
            gameObject.SetActive(false);
            collider.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            
        }
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