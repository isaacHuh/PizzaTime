using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyControl : MonoBehaviour
{
    public Text budget;
    public Text rent;
    public Text fines;
    public Text income;

    public float money = 0f;

    // Start is called before the first frame update
    void Start()
    {
        money += float.Parse(income.text);
        Subtract(float.Parse(rent.text));
        Subtract(float.Parse(fines.text));
    }

    // Update is called once per frame
    void Update()
    {
        budget.text = money.ToString("000");
    }

    public void Subtract(float amount)
    {
        money -= amount;
    }
}
