using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEditor;
using System;
using System.Diagnostics;
using System.Collections.Specialized;
using TMPro;

public class PizzaShop : MonoBehaviour
{
    int num_deliveries;
    System.Random random = new System.Random();
    public GameObject deliveryLocation;
    public GameObject LevelControl;
    List<GameObject> buildings = new List<GameObject>();
    public List<Vector2Int> deliveryPositions = new List<Vector2Int>();
    public TMP_Text pressE;

    void Start()
    {
        num_deliveries = random.Next(1, 3);
        UnityEngine.Debug.Log(num_deliveries);
        buildings = LevelControl.GetComponent<LevelControl>().buildings;
        setPizzaShop();
    }   

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            freezeCarMovement(collider);
            pressE.text = "Press E to choose pizzas to deliver";
            createDeliveryPositions();
        }
    }
    
    public void createDeliveryPositions()
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
        Vector2Int pizzaShopPosition = buildings[randomBuilding].GetComponent<BuildingControl>().deliveryPos;
 
        if(!deliveryPositions.Contains(pizzaShopPosition))
        {
            transform.position = new Vector3(buildings[randomBuilding].GetComponent<BuildingControl>().deliveryPos.x * 10, 5, buildings[randomBuilding].GetComponent<BuildingControl>().deliveryPos.y * 10);
            return;    
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
