using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CostAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartC
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }
}
