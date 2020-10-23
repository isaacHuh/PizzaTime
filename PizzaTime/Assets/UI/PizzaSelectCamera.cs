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

    public GameObject player;

    public PizzaShop pizzaShop;

    bool canSelect;
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
            pressE.text = "Press E to choose pizzas to deliver";
            pressQ.text = "";
            switchCameraBack();
            //gameObject.SetActive(false);
            //collider.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            
        }
    }
    void Update(){
        if(canSelect){
            if (Input.GetKeyDown(KeyCode.E))
            {
                pressE.text = "";
                pressQ.text = "Press Q to quit";
                switchCamera();
                
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                pressE.text = "Press E to choose pizzas to deliver";
                pressQ.text = "";
                switchCameraBack();
                gameObject.SetActive(false);
                //collider.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                
            }
        }else{


        }

    }

    private void onTriggerExit(Collider collider){
        if (collider.gameObject.tag == "Player"){
            pressE.text = "";
            pressQ.text = "";
            canSelect = false;
        }
    }

    private void onTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Player"){
            pressE.text = "Press E to choose pizzas to deliver";
            pressQ.text = "";
            canSelect = true;
        }
    }

    void switchCamera()
    {
        //pizzaShop.createDeliveryPositions();
        player.GetComponent<Car_Control>().enabled = false;
        mainCamera.transform.position = topDown.transform.position;
        mainCamera.transform.rotation = topDown.transform.rotation;
    }

    void switchCameraBack()
    {
        player.GetComponent<Car_Control>().enabled = true;
        mainCamera.transform.position = ogView.transform.position;
        mainCamera.transform.rotation = ogView.transform.rotation;
    }

}