using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

public class PizzaShop : MonoBehaviour
{
    public List<Vector2Int> deliveryQueue = new List<Vector2Int>();
    public GameObject topDown;
    public GameObject ogView;
    public Camera mainCamera;
    public TMP_Text pressE;
    public TMP_Text pressQ;

    public GameObject player;
    public GameObject deliveryChoice;
    public LevelControl levelControl;

    bool canSelect;
    List<GameObject> deliveries = new List<GameObject>();

    void Update(){
        if(canSelect){
            ChooseDelivery();
            if (Input.GetKeyDown(KeyCode.E) && pressE.text != "")
            {
                pressE.text = "";
                pressQ.text = "Press Q to quit";
                switchCamera();
                CreateChoiceDeliveries();

                Rigidbody rb = player.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            if (Input.GetKeyDown(KeyCode.Q) && pressE.text == "")
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                pressQ.text = "";
                switchCameraBack();
                DestroyChoiceDeliveries();

                if(deliveryQueue.Count > 0){
                    canSelect = false;
                    gameObject.SetActive(false);
                }else{
                    pressE.text = "Press E to choose pizzas to deliver";
                }

                //collider.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                
            }
        }

    }

    void ChooseDelivery()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Delivery")
                {
                    deliveryQueue.Add(hit.transform.GetComponent<ChooseDeliveries>().deliveryPos);
                    Destroy(hit.collider.transform.gameObject);
                }
            }
        }
    }

    void CreateChoiceDeliveries(){

        int numDeliveries = Random.Range(1,5);
        for(int i = 0; i < numDeliveries; i++){
            GameObject building = levelControl.buildings[Random.Range(0,levelControl.buildings.Count)];

            Vector2Int deliveryPos = building.GetComponent<BuildingControl>().deliveryPos;
            GameObject delivery = Instantiate(deliveryChoice, new Vector3(10*deliveryPos[0],5,10*deliveryPos[1]), Quaternion.identity);
            delivery.GetComponent<ChooseDeliveries>().deliveryPos = deliveryPos;
            deliveries.Add(delivery);
        }
    }

    void DestroyChoiceDeliveries(){
        foreach(GameObject delivery in deliveries){
            Destroy(delivery);
        }

        deliveries.Clear();
    }
    void OnTriggerExit(Collider collider){
        if (collider.gameObject.tag == "Player"){
            pressE.text = "";
            pressQ.text = "";
            canSelect = false;
        }
    }

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Player"){
            pressE.text = "Press E to choose pizzas to deliver";
            pressQ.text = "";
            canSelect = true;
        }
    }

    void switchCamera()
    {
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