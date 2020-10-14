using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelClickController : MonoBehaviour
{
    public bool mouseEffect = false;
    Vector3 startPos;
    public string sceneLink = "";
    private void Start()
    {
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!mouseEffect)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, startPos, 0.1f);
        }
        else {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, startPos + new Vector3(-0.4f, 0, 0), 0.1f);
        }

        //mouseEffect = false;
    }

    private void OnMouseEnter()
    {
        Debug.Log("enter");
        mouseEffect = true;
    }

    private void OnMouseExit()
    {
        mouseEffect = false;
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneLink);
    }

}
