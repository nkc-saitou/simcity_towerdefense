using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShop : ObjectBase
{
    [SerializeField]
    protected int addMoney = 10;

    Money money;

    float time = 0.0f;
    float speed = 1.0f;

    float addTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        money = FindObjectOfType<Money>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * speed;
        if(time >= addTime)
        {
            money.AddMoney(addMoney);
            time = 0.0f;
        }
    }
}
