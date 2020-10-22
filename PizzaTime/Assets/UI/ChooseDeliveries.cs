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
    public GameObject deliveryObject;
    
    void Update()
    {
        ChooseDelivery();
    }

    public void ChooseDelivery()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Delivery(Clone)")
                {
                    deliveryPositionQueue.Add(hit.collider.transform.position);
                    if (deliveryPositionQueue.Count > 1)
                    {
                        Destroy(hit.collider.transform.gameObject);
                    }
                }
            }
        }
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
            else if(deliveryPositionQueue.Count == 0)
            {
                UnityEngine.Debug.Log("Empty List");
                deliveryPositionQueue = null;
            }
        }
    }
}
