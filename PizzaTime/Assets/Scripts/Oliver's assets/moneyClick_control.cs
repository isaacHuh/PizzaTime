using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyClick_control : MonoBehaviour
{

    public bool mouseEffect = false;
    public float value;
    public MoneyManager money; 

    Vector3 startPos;
    private bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        money = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!pressed)
        {
            if (!mouseEffect)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, startPos, 0.1f);
            }
            else
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, startPos + new Vector3(-0.4f, 0, 0), 0.1f);
            }
        }
        else
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, startPos + new Vector3(2f, 0, 0), 0.1f);
        }

    }

    private void OnMouseEnter()
    {
        mouseEffect = true;
    }

    private void OnMouseExit()
    {
        mouseEffect = false;
    }

    private void OnMouseDown()
    {
        if (!pressed)
        {
            money.budget -= value;
            pressed = true;
        }
    }
}
