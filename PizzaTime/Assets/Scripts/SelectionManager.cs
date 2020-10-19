using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name);
            Debug.Log(Input.mousePosition);

            var selection = hit.transform;

            if (selection.tag == "LevelClick")
            {
                selection.GetComponent<LevelClickController>().mouseEffect = true;
            }
        }
    }
}
