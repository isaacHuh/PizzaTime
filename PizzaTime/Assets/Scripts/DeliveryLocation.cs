using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryLocation : MonoBehaviour
{
    public PizzaShop pizzaShop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pizzaShop.deliveryQueue.Count > 0){
            Vector2Int deliveryPos = pizzaShop.deliveryQueue[0];
            transform.position = new Vector3(deliveryPos[0]*10, 5, deliveryPos[1]*10);
        }else{
            transform.position = new Vector3(0,-100,0);
            pizzaShop.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            MoneyManager moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
            moneyManager.budget += 50;
            moneyManager.income += 50;

            transform.position += new Vector3(0,-100f,0);
            pizzaShop.deliveryQueue.RemoveAt(0);
        }
    }
}
