using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainRoomUIControl : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text moneyText;
    public MoneyManager moneyManager;
    void Start()
    {
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        moneyManager.fines = 0;
        moneyManager.income = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + moneyManager.budget.ToString();
    }
}
