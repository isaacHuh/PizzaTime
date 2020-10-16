using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEditor;

public class GetDeliveryPositions : MonoBehaviour
{
    int num_deliveries;
    public GameObject deliveryLocation;
    public GameObject LevelControl;
    List<GameObject> buildings = new List<GameObject>();


    void Start()
    {
        num_deliveries = Random.Range(0, 3);
        buildings = LevelControl.GetComponent<LevelControl>().buildings;
    }
        
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            createDeliveryPositions();
        }

    }
    
    void createDeliveryPositions()
    {
        
        
        for (int i = 0; i < num_deliveries; i++)
        {
            int randomBuilding = Random.Range(0,buildings.Count-1);
            Vector2Int deliveryPos = buildings[randomBuilding].GetComponent<BuildingControl>().deliveryPos;
            Debug.Log(deliveryPos);
            GameObject position = Instantiate(deliveryLocation, new Vector3(deliveryPos.x, deliveryPos.y, 0), Quaternion.identity);
        }
    }
}
