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

    public MoneyManager money;

    // Start is called before the first frame update
    void Start()
    {
        money = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        budget.text = money.budget.ToString();
        rent.text = money.rent.ToString();
        fines.text = money.fines.ToString();
        income.text = money.income.ToString();
    }

}
