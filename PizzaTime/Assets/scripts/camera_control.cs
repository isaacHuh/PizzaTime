using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_control : MonoBehaviour
{

    public GameObject interior;
    public GameObject hood;
    public GameObject overhead;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = overhead.transform.position;
            transform.rotation = overhead.transform.rotation;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = hood.transform.position;
            transform.rotation = hood.transform.rotation;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = interior.transform.position;
            transform.rotation = interior.transform.rotation;
        }
    }
}
