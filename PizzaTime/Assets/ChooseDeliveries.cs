using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class ChooseDeliveries : MonoBehaviour
{
    //List<Vector2Int> deliveryPositions = GetComponent<PizzaShop>.deliveryPositions;
    public List<Vector3> deliveryPositionQueue = new List<Vector3>();
    public Camera topDownCamera;
    public Material highlightMaterial;
    public GameObject deliveryObject;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ChooseDelivery();
    }

    void ChooseDelivery()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = topDownCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Location(Clone)")
                {
                    deliveryPositionQueue.Add(hit.collider.transform.position);
                    //gameObject.GetComponent<Renderer>().material = highlightMaterial;
                    if (deliveryPositionQueue.Count > 1)
                    {
                        Destroy(hit.collider.transform.gameObject);
                    }
                    else
                    {

                    }
                }
            }
        }
        //GameObject currentDelivery = Instantiate(deliveryLocation, deliveryPositionQueue[0], Quaternion.identity);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            if (deliveryPositionQueue.Count > 1)
            {
                deliveryPositionQueue.RemoveAt(0);
                
                GameObject currentDelivery = Instantiate(deliveryObject, deliveryPositionQueue[0], Quaternion.identity);
            }
            else
            {
                deliveryPositionQueue = null;
            }
        }
    }
}
