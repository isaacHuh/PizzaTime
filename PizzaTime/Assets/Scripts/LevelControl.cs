﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public static int level = 0;
    public int arraySize = 10;
    public int[,] levelArray;
    public Node[,] nodeArray;

    public List<GameObject> blocks;
    public GameObject node;
    public float blockSize = 5;

    public List<Vector2> openSpots = new List<Vector2>();

    public Vector2Int startPos = new Vector2Int();
    public Vector2Int endPos = new Vector2Int();

    public GameObject player;
    private void Awake()
    {
        LevelControl.level++;
        levelArray = new int[arraySize, arraySize];
        nodeArray = new Node[arraySize, arraySize];

        player.transform.position = new Vector3((int)(arraySize/2 * blockSize), 5, (int)(arraySize/2) * blockSize);

        while (openSpots.Count < (arraySize * arraySize)/2)
        {
            CreateLevel();
        }

        CreateMap();
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        LevelGenerate.level++;
        levelArray = new int[arraySize, arraySize];

        while (openSpots.Count < 10)
        {
            createLevel();
        }

        createMap();
        */

        //createEnemies(LevelGeneration.level);

        //createEnd();
    }

    private void Update()
    {
        // for debugging
        if (Input.GetKeyDown(KeyCode.P)) {
            List<Node> path = FindPath(startPos, endPos);
            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    if (nodeArray[i, j] == null) {
                        continue;
                    }
                    Vector3 pos = nodeArray[i, j].transform.localPosition;
                    pos.y = 0;
                    nodeArray[i, j].transform.localPosition = pos;
                }
            }

            foreach (Node obj in path) {
                Vector3 pos = obj.transform.localPosition;
                pos.y = -5;
                obj.transform.localPosition = pos;
            }
        }
    
    }

    public void DisplayPath(List<Node> path) {
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                if (nodeArray[i, j] == null)
                {
                    continue;
                }
                Vector3 pos = nodeArray[i, j].transform.localPosition;
                pos.y = 0;
                nodeArray[i, j].transform.localPosition = pos;
            }
        }

        foreach (Node obj in path)
        {
            Vector3 pos = obj.transform.localPosition;
            pos.y = -5;
            obj.transform.localPosition = pos;
        }
    }


    private void CreateLevel()
    {
        openSpots.Clear();
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                levelArray[i, j] = -1;
            }
        }

        int x = arraySize / 2;
        int y = arraySize / 2;

        levelArray[x, y] = 1;

        //player.transform.position = new Vector3(x * blockSize, 0, y * blockSize);

        ChooseType(x - 1, y);
        ChooseType(x + 1, y);
        ChooseType(x, y - 1);
        ChooseType(x, y + 1);

    }

    public int[,] GetLevelArray() {
        return levelArray;
    }

    private void ChooseType(int x, int y)
    {
        int randomType = Random.Range(0, 2);

        if (x < 0 || x >= arraySize)
        {
            return;
        }

        if (y < 0 || y >= arraySize)
        {
            return;
        }


        if (levelArray[x, y] != -1)
        {
            return;
        }

        levelArray[x, y] = randomType;

        if (randomType == 0)
        {
            return;
        }
        else
        {
            openSpots.Add(new Vector2(x, y));
        }

        ChooseType(x - 1, y);
        ChooseType(x + 1, y);
        ChooseType(x, y - 1);
        ChooseType(x, y + 1);
    }

    private void CreateMap()
    {
        //create buildings
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                if (levelArray[i, j] != 1)
                {
                    if (levelArray[i, Mathf.Clamp(j + 1, 0, arraySize - 1)] == 1 || 
                        levelArray[i, Mathf.Clamp(j - 1, 0, arraySize - 1)] == 1 ||
                        levelArray[Mathf.Clamp(i + 1, 0, arraySize - 1), j] == 1 ||
                        levelArray[Mathf.Clamp(i - 1, 0, arraySize - 1), j] == 1) {

                        Quaternion angle = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);
                        Instantiate(blocks[Random.Range(0, blocks.Count)], transform.position + new Vector3(i * blockSize, -1.1f, j * blockSize), angle, transform);
                    }
                }
            }
        }

        //create nodes
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                nodeArray[i, j] = null;
                if (levelArray[i, j] == 1)
                {
                    GameObject newNode = Instantiate(node, transform.position + new Vector3(i * blockSize, -1.1f, j * blockSize), Quaternion.identity, transform);
                    newNode.GetComponent<Node>().arrayPos = new Vector2Int(i,j);
                    nodeArray[i, j] = newNode.GetComponent<Node>();
                }
            }
        }

        //assign adj nodes
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                if (nodeArray[i, j] == null) {
                    continue;
                }

                Node currNode = nodeArray[i, j];

                if (i - 1 >= 0)
                {
                    if (nodeArray[i - 1, j] != null)
                    {
                        currNode.adjNodes.Add(nodeArray[i - 1, j]);
                    }
                }
                if (i + 1 < arraySize)
                {
                    if (nodeArray[i + 1, j] != null)
                    {
                        currNode.adjNodes.Add(nodeArray[i + 1, j]);
                    }
                }
                if (j - 1 >= 0)
                {
                    if (nodeArray[i, j - 1] != null)
                    {
                        currNode.adjNodes.Add(nodeArray[i, j - 1]);
                    }
                }
                if (j + 1 < arraySize)
                {
                    if (nodeArray[i, j + 1] != null)
                    {
                        currNode.adjNodes.Add(nodeArray[i, j + 1]);
                    }
                }

            }
        }
    }

    public List<Node> FindPath(Vector2Int startPos, Vector2Int endPos) {
        //reset nodes
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                if (nodeArray[i, j] == null)
                {
                    continue;
                }
                Node resetNode = nodeArray[i, j];
                resetNode.pathWeight = -1;
                resetNode.parentNode = null;
            }
        }

        Node startNode = nodeArray[startPos[0], startPos[1]];
        if (startNode == null) {
            return new List<Node>();
        }
        startNode.pathWeight = 0;
        Node endNode = nodeArray[endPos[0], endPos[1]];

        List<Node> queuedNodes = new List<Node>();
        queuedNodes.Add(startNode);

        // calculate path weight of each node
        while (queuedNodes.Count > 0) {
            if (queuedNodes[0] == endNode) {
                queuedNodes.RemoveAt(0);
                break;
            }

            Node currNode = queuedNodes[0];
            queuedNodes.RemoveAt(0);

            foreach (Node adjNode in currNode.adjNodes) {
                if (adjNode.pathWeight == -1 || currNode.pathWeight + currNode.nodeWeight < adjNode.pathWeight) {
                    adjNode.pathWeight = currNode.pathWeight + currNode.nodeWeight;
                    adjNode.parentNode = currNode;


                    queuedNodes.Add(adjNode);
                }
            }
        }

        List<Node> path = new List<Node>();
        Node pathAdd = endNode;

        // create path list by tracing back
        while (pathAdd != null) {
            path.Insert(0, pathAdd);
            if (pathAdd == startNode) {
                pathAdd = null;
                continue;
            }

            pathAdd = pathAdd.parentNode;
        }

        return path;
    }
}