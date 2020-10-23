using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 newRotation = new Vector3(-90, 0, 0);
        transform.eulerAngles = newRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
