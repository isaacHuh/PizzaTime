using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimation : MonoBehaviour
{
    Vector3 endPos = new Vector3(0,-2.5f,-64);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.z < -50) {
            gameObject.transform.eulerAngles = Vector3.Lerp(gameObject.transform.eulerAngles, new Vector3(0, 305, 0), 0.02f);
        }

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPos, 0.02f);
    }
}
