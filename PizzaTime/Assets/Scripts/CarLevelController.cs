using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLevelController : MonoBehaviour
{
    public LevelControl levelControl;
    public PizzaShop pizzaShop;
    public Vector2Int endPoint;
    Vector2Int startPos, pizzaPoint;
    public List<Node> path;
    // Start is called before the first frame update
    void Start()
    {
        pizzaPoint = new Vector2Int((int)(transform.position.x / levelControl.blockSize), (int)(transform.position.z / levelControl.blockSize));
        endPoint = new Vector2Int((int)(transform.position.x / levelControl.blockSize), (int)(transform.position.z / levelControl.blockSize));
        startPos = new Vector2Int((int)(transform.position.x / levelControl.blockSize), (int)(transform.position.z / levelControl.blockSize));
    }

    // Update is called once per frame
    void Update()
    {
        endPoint = pizzaPoint;
        if(pizzaShop.deliveryQueue.Count > 0){
            endPoint = pizzaShop.deliveryQueue[0];
        }

        Vector2Int currPos = new Vector2Int((int)((transform.position.x + (levelControl.blockSize/2)) / levelControl.blockSize), (int)((transform.position.z + (levelControl.blockSize / 2)) / levelControl.blockSize));
        currPos[0] = Mathf.Clamp(currPos[0], 0, levelControl.arraySize - 1);
        currPos[1] = Mathf.Clamp(currPos[1], 0, levelControl.arraySize - 1);

        if (levelControl.nodeArray[currPos[0], currPos[1]] != null) {
            startPos = currPos;
        }
        path = levelControl.FindPath(startPos, endPoint);
        levelControl.DisplayPath(path);
    }
}
