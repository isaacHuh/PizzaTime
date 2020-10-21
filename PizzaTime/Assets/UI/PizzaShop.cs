using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEditor;
using System;
using System.Diagnostics;
using System.Collections.Specialized;

public class PizzaShop : MonoBehaviour
{
    int num_deliveries;
    System.Random random = new System.Random();
    public GameObject deliveryLocation;
    public GameObject LevelControl;
    List<GameObject> buildings = new List<GameObject>();
    List<Vector2Int> deliveryPositions = new List<Vector2Int>();

    void Start()
    {
        buildings = LevelControl.GetComponent<LevelControl>().buildings;
    }   

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            freezeCarMovement(collider);    
            num_deliveries = random.Next(1, 3);
            
            createDeliveryPositions();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            /*Vector3 deliveryPos = GetComponent<ChooseDeliveries>().GetDelivery();
            UnityEngine.Debug.Log(deliveryPos);*/
            
            //GameObject currentDelivery = Instantiate(deliveryLocation, firstDeliveryPos, Quaternion.identity);
        }
    }
    
    void createDeliveryPositions()
    {   
        for (int i = 0; i < num_deliveries; i++)
        {
            int randomBuilding = random.Next(0, buildings.Count - 1);
            Vector2Int deliveryPos = buildings[randomBuilding].GetComponent<BuildingControl>().deliveryPos;
            if (!deliveryPositions.Contains(deliveryPos) && deliveryPos != null)
            {
                GameObject position = Instantiate(deliveryLocation, new Vector3(deliveryPos.x * 10, 5, deliveryPos.y * 10), Quaternion.identity);
                deliveryPositions.Add(deliveryPos);
            }
        }
    }

    void setPizzaShop()
    {
        int randomBuilding = random.Next(0, buildings.Count - 1);
        Vector3 pizzaShopPosition = new Vector3(buildings[randomBuilding].GetComponent<BuildingControl>().deliveryPos.x * 10, 5, buildings[randomBuilding].GetComponent<BuildingControl>().deliveryPos.y * 10);
        for (int i = 0; i < num_deliveries; i++)
        {
            if(deliveryPositions[i].x != pizzaShopPosition.x && deliveryPositions[i].y != pizzaShopPosition.z)
            {
                transform.position = pizzaShopPosition;
            }
        }
        setPizzaShop();
    }

    void freezeCarMovement(Collider collider)
    {
        collider.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        collider.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        collider.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
