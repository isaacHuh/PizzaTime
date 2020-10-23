using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class ChooseDeliveries : MonoBehaviour
{
    //List<Vector2Int> deliveryPositions = GetComponent<PizzaShop>.deliveryPositions;

    public Vector2Int deliveryPos;
    public GameObject pizzaShop;

    void Update()
    {
        //ChooseDelivery();
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
                    pizzaShop.GetComponent<PizzaShop>().deliveryQueue.Add(deliveryPos);
                    Destroy(hit.collider.transform.gameObject);
                }
            }
        }
    }

}
