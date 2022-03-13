using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using UnityEngine.UI;
using TMPro;

public class ObjectCreator : MonoBehaviour
{
    public Action ActObjectCreateCallBack;
    public Func<string> FuncSetObjectName;

    [SerializeField]
    SquaresData squaresData;

    [SerializeField]
    CameraController cameraController;

    List<GameObject> createObjectList = new List<GameObject>();

    Color createObjectBaseColor = Color.white;

    float offset_x = 999.5f;
    float offset_y = 0.5f;
    float offset_z = 999.5f;

    float posGulid_x = 0.5f;
    float posGulid_y = 0.5f;
    float posGulid_z = 0.5f;

    string objectName = "";

    int index_x = 0;
    int index_z = 0;

    GameObject assetLoadObject = null;

    GameObject createObject = null;
    ObjectBase objbase = null;

    bool isMouseFollow = false;

    private void FixedUpdate()
    {
        // カメラを動かしている際はオブジェクトの移動停止
        if (Input.GetMouseButton(1)) return;

        // オブジェクトの生成が行われていたら
        if (isMouseFollow)
        {
            ObjectMove();
            bool checkSquares = squaresData.CheckSquares(index_x, index_z, objbase.SquaresCount_x, objbase.SquaresCount_y);

            // 現在オブジェクトがある位置にすでに他のオブジェクトがある
            if (checkSquares == false)
            {
                // マテリアルカラーを赤に変更
                createObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
                return;
            }
            else createObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_BaseColor", createObjectBaseColor);
 
            // マウス左クリックでオブジェクト配置
            if (Input.GetMouseButton(0))
            {
                isMouseFollow = false;
                createObject = null;
                // オブジェクト配置位置の内部データを更新
                squaresData.UpdateSquaresStatus(index_x, index_z, objbase.SquaresCount_x, objbase.SquaresCount_y);

                if(ActObjectCreateCallBack != null) ActObjectCreateCallBack();
            }
        }
    }

    /// <summary>
    /// 設置
    /// </summary>
    public void OnButtonDown_Create()
    {
        if (isMouseFollow) return;
        if (FuncSetObjectName == null) return;

        // アセットロード
        Addressables.LoadAssetAsync<GameObject>(FuncSetObjectName()).Completed += obj =>
        {
            // オブジェクト生成
            createObject = Instantiate(obj.Result);
            objbase = createObject.GetComponent<ObjectBase>();
            isMouseFollow = true;

            createObjectBaseColor = createObject.transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_BaseColor");
        };
    }

    /// <summary>
    /// 撤去
    /// </summary>
    public void Removal()
    {

    }

    /// <summary>
    /// オブジェクト配置移動
    /// </summary>
    void ObjectMove()
    {
        //Cubeの位置をワールド座標からスクリーン座標に変換して、objectPointに格納
        Vector3 objectPoint
            = Camera.main.WorldToScreenPoint(createObject.transform.position);

        //Cubeの現在位置(マウス位置)を、pointScreenに格納
        Vector3 pointScreen
            = new Vector3(Input.mousePosition.x,
                          offset_y,
                          Input.mousePosition.y/4);

        //Cubeの現在位置を、スクリーン座標からワールド座標に変換して、pointWorldに格納
        Vector3 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
        pointWorld.y = offset_y;
        pointWorld.z *= 2;

        // マス目に沿って移動
        pointWorld.x = Mathf.Ceil(pointWorld.x) + posGulid_x;
        pointWorld.z = Mathf.Ceil(pointWorld.z) + posGulid_z;

        //Cubeの位置を、pointWorldにする
        createObject.transform.position = pointWorld;

        //配列の添え字を計算
        index_x = (int)(pointWorld.x + offset_x);
        index_z = (int)(pointWorld.z + offset_z);
    }

}
