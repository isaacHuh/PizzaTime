using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public CarLevelController player;
    Vector3 currRot;
    Vector3 targetRot;
    // Start is called before the first frame update
    void Start()
    {
        targetRot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.path.Count >= 3)
        {
            transform.LookAt(player.path[2].transform.position);
            Vector3 rot = transform.rotation.eulerAngles;
            rot.x = 0;
            rot.z = 0;

            targetRot = rot;
        }

        currRot = Vector3.Lerp(currRot, targetRot, 0.2f);
        transform.rotation = Quaternion.Euler(currRot);
    }
}
