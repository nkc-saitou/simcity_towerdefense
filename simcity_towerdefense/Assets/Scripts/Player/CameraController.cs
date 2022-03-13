using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject followObject = null;     // 視点となるオブジェクト
    [SerializeField]
    float lookPlayDistance = 0.3f;   // 視点の遊び
    [SerializeField]
    float followSmooth = 4.0f;       // 追いかけるときの速度

    float cameraPlayDiatance = 0.3f;    // 視点からカメラまでの距離の遊び
    float leaveSmooth = 20.0f;          // 離れるときの速度


    float cameraDistance = 2.5f;        // 視点からカメラまでの距離
    public float CameraDistance { get { return cameraDistance; } }
    

    float cameraHeight = 1.0f;          // デフォルトのカメラの高さ
    float currentCameraHeight = 1.0f;          // 現在のカメラの高さ

    float cameraHeightMin = 0.1f;    // カメラの最低の高さ
    float cameraHeightMax = 3.0f;    // カメラの最大の高さ

    float scrollWheel = 2.5f;

    Vector3 lookPos = Vector3.zero;     // 実際にカメラを向ける座標

    void FixedUpdate()
    {
        if (followObject == null) return;

        if(Input.GetMouseButton(1))
        {
            float xPos = Input.GetAxis("Mouse X") * -1 * 2;
            float yPos = Input.GetAxis("Mouse Y") * -1 * 2;
            Roll(xPos, yPos);
        }

        float scrollDiff = Input.GetAxis("Mouse ScrollWheel") * 15 * -1;
        scrollWheel += scrollDiff;
        cameraDistance = scrollWheel;

        if (Mathf.Abs(scrollDiff) > 0)
        {
            cameraHeightMax += scrollDiff;
            float xPos = 0.0f;
            float yPos = Input.GetAxis("Mouse Y") * -1;
            Roll(xPos, yPos);
        }

        UpdateLookPosition();
        UpdateCameraPosition();

        transform.LookAt(lookPos);
    }

    void UpdateLookPosition()
    {
        // 目標の視点と現在の視点の距離を求める
        Vector3 vec = followObject.transform.position - lookPos;
        float distance = vec.magnitude;

        if (distance > lookPlayDistance)
        {   // 遊びの距離を超えていたら目標の視点に近づける
            float move_distance = (distance - lookPlayDistance) * (Time.deltaTime * followSmooth);
            lookPos += vec.normalized * move_distance;
        }
    }

    void UpdateCameraPosition()
    {
        // XZ平面におけるカメラと視点の距離を取得する
        Vector3 xz_vec = followObject.transform.position - transform.position;
        xz_vec.y = 0;
        float distance = xz_vec.magnitude;

        // カメラの移動距離を求める
        float move_distance = 0;
        if (distance > cameraDistance + cameraPlayDiatance)
        {   // カメラが遊びを超えて離れたら追いかける
            move_distance = distance - (cameraDistance + cameraPlayDiatance);
            move_distance *= Time.deltaTime * followSmooth;
        }
        else if (distance < cameraDistance - cameraPlayDiatance)
        {   // カメラが遊びを超えて近づいたら離れる
            move_distance = distance - (cameraDistance - cameraPlayDiatance);
            move_distance *= Time.deltaTime * leaveSmooth;
        }

        // 新しいカメラの位置を求める
        Vector3 camera_pos = transform.position + (xz_vec.normalized * move_distance);

        // 高さは常に現在の視点からの一定の高さを維持する
        camera_pos.y = lookPos.y + currentCameraHeight;

        transform.position = camera_pos;
    }

    public void Roll(float x, float y)
    {
        // 移動前の距離を保存する
        float prev_distance = Vector3.Distance(followObject.transform.position, transform.position);
        Vector3 pos = transform.position;

        // 横に移動する
        pos += transform.right * x;

        // 縦に移動する
        currentCameraHeight = Mathf.Clamp(currentCameraHeight + y, cameraHeightMin, cameraHeightMax);
        pos.y = lookPos.y + currentCameraHeight;

        // 移動後の距離を取得する
        float after_distance = Vector3.Distance(followObject.transform.position, pos);

        // 視点を対象に向けて近づける（遊びをなくす）
        lookPos = Vector3.Lerp(lookPos, followObject.transform.position, 0.1f);

        // カメラの更新
        transform.position = pos;
        transform.LookAt(lookPos);

        // 平行移動により若干距離が変わるので補正する
        transform.position += transform.forward * (after_distance - prev_distance);
    }
}
