using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI moneyText;

    [SerializeField]
    PlayerData playerData;

    public int NowMoney { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = playerData.InitMoney.ToString();
        NowMoney = playerData.InitMoney;
    }

    public void AddMoney(int v)
    {
        NowMoney += v;
    }

    public void RemoveMoney(int v)
    {
        NowMoney -= v;
    }
}
