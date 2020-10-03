using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector2Int arrayPos;
    public List<Node> adjNodes = new List<Node>();
    public Node parentNode = null;
    public int pathWeight = -1;
    public int nodeWeight = 1;
}
