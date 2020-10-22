using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    public float speed = 10;
    public bool verticle = true;
    public bool forward = true;
    public float endPoint = 0;

    private float dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (forward)
            dir = 1;
        else
            dir = -1;

    }

    // Update is called once per frame
    void Update()
    {
        if (verticle && transform.localPosition.y < endPoint)
            transform.localPosition += Vector3.up * Time.deltaTime * speed;

        if (!verticle && transform.localPosition.x < endPoint)
            transform.localPosition += (dir * Vector3.right) * Time.deltaTime * speed;
    }
}