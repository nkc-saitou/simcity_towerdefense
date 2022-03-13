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
        // �J�����𓮂����Ă���ۂ̓I�u�W�F�N�g�̈ړ���~
        if (Input.GetMouseButton(1)) return;

        // �I�u�W�F�N�g�̐������s���Ă�����
        if (isMouseFollow)
        {
            ObjectMove();
            bool checkSquares = squaresData.CheckSquares(index_x, index_z, objbase.SquaresCount_x, objbase.SquaresCount_y);

            // ���݃I�u�W�F�N�g������ʒu�ɂ��łɑ��̃I�u�W�F�N�g������
            if (checkSquares == false)
            {
                // �}�e���A���J���[��ԂɕύX
                createObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
                return;
            }
            else createObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_BaseColor", createObjectBaseColor);
 
            // �}�E�X���N���b�N�ŃI�u�W�F�N�g�z�u
            if (Input.GetMouseButton(0))
            {
                isMouseFollow = false;
                createObject = null;
                // �I�u�W�F�N�g�z�u�ʒu�̓����f�[�^���X�V
                squaresData.UpdateSquaresStatus(index_x, index_z, objbase.SquaresCount_x, objbase.SquaresCount_y);

                if(ActObjectCreateCallBack != null) ActObjectCreateCallBack();
            }
        }
    }

    /// <summary>
    /// �ݒu
    /// </summary>
    public void OnButtonDown_Create()
    {
        if (isMouseFollow) return;
        if (FuncSetObjectName == null) return;

        // �A�Z�b�g���[�h
        Addressables.LoadAssetAsync<GameObject>(FuncSetObjectName()).Completed += obj =>
        {
            // �I�u�W�F�N�g����
            createObject = Instantiate(obj.Result);
            objbase = createObject.GetComponent<ObjectBase>();
            isMouseFollow = true;

            createObjectBaseColor = createObject.transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_BaseColor");
        };
    }

    /// <summary>
    /// �P��
    /// </summary>
    public void Removal()
    {

    }

    /// <summary>
    /// �I�u�W�F�N�g�z�u�ړ�
    /// </summary>
    void ObjectMove()
    {
        //Cube�̈ʒu�����[���h���W����X�N���[�����W�ɕϊ����āAobjectPoint�Ɋi�[
        Vector3 objectPoint
            = Camera.main.WorldToScreenPoint(createObject.transform.position);

        //Cube�̌��݈ʒu(�}�E�X�ʒu)���ApointScreen�Ɋi�[
        Vector3 pointScreen
            = new Vector3(Input.mousePosition.x,
                          offset_y,
                          Input.mousePosition.y/4);

        //Cube�̌��݈ʒu���A�X�N���[�����W���烏�[���h���W�ɕϊ����āApointWorld�Ɋi�[
        Vector3 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
        pointWorld.y = offset_y;
        pointWorld.z *= 2;

        // �}�X�ڂɉ����Ĉړ�
        pointWorld.x = Mathf.Ceil(pointWorld.x) + posGulid_x;
        pointWorld.z = Mathf.Ceil(pointWorld.z) + posGulid_z;

        //Cube�̈ʒu���ApointWorld�ɂ���
        createObject.transform.position = pointWorld;

        //�z��̓Y�������v�Z
        index_x = (int)(pointWorld.x + offset_x);
        index_z = (int)(pointWorld.z + offset_z);
    }

}
