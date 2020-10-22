using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEditor;
using System;
using System.Diagnostics;

public class GetDeliveryPositions : MonoBehaviour
{
    int num_deliveries;
    System.Random random = new System.Random();
    public GameObject deliveryLocation;
    public GameObject LevelControl;
    List<GameObject> buildings = new List<GameObject>();

    void Start()
    {
        buildings = LevelControl.GetComponent<LevelControl>().buildings;
    }
        
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            num_deliveries = random.Next(1, 3);
            createDeliveryPositions();
        }

    }
    
    void createDeliveryPositions()
    {
        for (int i = 0; i < num_deliveries; i++)
        {
            int randomBuilding = random.Next(0,buildings.Count-1);
            Vector2Int deliveryPos = buildings[randomBuilding].GetComponent<BuildingControl>().deliveryPos;
            for(int j = 0; j < buildings.Count; j++)
            {
                GameObject position = Instantiate(deliveryLocation, new Vector3(deliveryPos.x*10, 5, deliveryPos.y*10), Quaternion.identity);
            }

        }
    }
}
