using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;
using UnityEngine.UI;

public class ObjectCreateWindow : MonoBehaviour
{
    [SerializeField]
    GameObject createWindow;

    [SerializeField]
    ObjectCreator objectCreator;

    [SerializeField]
    Money money;

    [SerializeField]
    TextMeshProUGUI moneyText;

    [SerializeField]
    TextMeshProUGUI nowMoneyText;

    [SerializeField]
    Button createButton;

    AsyncOperationHandle<GameObject> asset;

    int cost = 0;

    string objectName = "ObjectShop";

    // Start is called before the first frame update
    void Start()
    {
        objectCreator.ActObjectCreateCallBack += OnButtonDown_WindowActive;
        objectCreator.ActObjectCreateCallBack += ObjectCreateChangeMoney;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            OnButtonDown_WindowActive();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnButtonDown_WindowHide();
        }

        nowMoneyText.text = money.NowMoney.ToString();
    }

    public void OnButtonDown_WindowHide()
    {
        createWindow.SetActive(false);
    }

    public void OnButtonDown_WindowActive()
    {
        createWindow.SetActive(true);
        OnButtonDown_SelectObject(objectName);
    }

    public void ObjectCreateChangeMoney()
    {
        Debug.Log(cost);
        money.RemoveMoney(cost);
    }

    /// <summary>
    /// 作成するオブジェクト選択
    /// </summary>
    /// <param name="selectObjectName"></param>
    public void OnButtonDown_SelectObject(string selectObjectName)
    {
        objectCreator.FuncSetObjectName = () => { return selectObjectName; };
        objectName = selectObjectName;
        // アセットロード
        asset = Addressables.LoadAssetAsync<GameObject>(selectObjectName);
        asset.Completed += obj =>
        {
            ObjectBase objbase = obj.Result.GetComponent<ObjectBase>();
            moneyText.text = objbase.Cost.ToString();
            cost = objbase.Cost;
            if (money.NowMoney < objbase.Cost)
            {
                createButton.interactable = false;
                moneyText.color = Color.red;
            }
            else
            {
                createButton.interactable = true;
                moneyText.color = Color.black;
            }
        };
    }
}
