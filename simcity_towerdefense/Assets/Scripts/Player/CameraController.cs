using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject followObject = null;     // ���_�ƂȂ�I�u�W�F�N�g
    [SerializeField]
    float lookPlayDistance = 0.3f;   // ���_�̗V��
    [SerializeField]
    float followSmooth = 4.0f;       // �ǂ�������Ƃ��̑��x

    float cameraPlayDiatance = 0.3f;    // ���_����J�����܂ł̋����̗V��
    float leaveSmooth = 20.0f;          // �����Ƃ��̑��x


    float cameraDistance = 2.5f;        // ���_����J�����܂ł̋���
    public float CameraDistance { get { return cameraDistance; } }
    

    float cameraHeight = 1.0f;          // �f�t�H���g�̃J�����̍���
    float currentCameraHeight = 1.0f;          // ���݂̃J�����̍���

    float cameraHeightMin = 0.1f;    // �J�����̍Œ�̍���
    float cameraHeightMax = 3.0f;    // �J�����̍ő�̍���

    float scrollWheel = 2.5f;

    Vector3 lookPos = Vector3.zero;     // ���ۂɃJ��������������W

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
        // �ڕW�̎��_�ƌ��݂̎��_�̋��������߂�
        Vector3 vec = followObject.transform.position - lookPos;
        float distance = vec.magnitude;

        if (distance > lookPlayDistance)
        {   // �V�т̋����𒴂��Ă�����ڕW�̎��_�ɋ߂Â���
            float move_distance = (distance - lookPlayDistance) * (Time.deltaTime * followSmooth);
            lookPos += vec.normalized * move_distance;
        }
    }

    void UpdateCameraPosition()
    {
        // XZ���ʂɂ�����J�����Ǝ��_�̋������擾����
        Vector3 xz_vec = followObject.transform.position - transform.position;
        xz_vec.y = 0;
        float distance = xz_vec.magnitude;

        // �J�����̈ړ����������߂�
        float move_distance = 0;
        if (distance > cameraDistance + cameraPlayDiatance)
        {   // �J�������V�т𒴂��ė��ꂽ��ǂ�������
            move_distance = distance - (cameraDistance + cameraPlayDiatance);
            move_distance *= Time.deltaTime * followSmooth;
        }
        else if (distance < cameraDistance - cameraPlayDiatance)
        {   // �J�������V�т𒴂��ċ߂Â����痣���
            move_distance = distance - (cameraDistance - cameraPlayDiatance);
            move_distance *= Time.deltaTime * leaveSmooth;
        }

        // �V�����J�����̈ʒu�����߂�
        Vector3 camera_pos = transform.position + (xz_vec.normalized * move_distance);

        // �����͏�Ɍ��݂̎��_����̈��̍������ێ�����
        camera_pos.y = lookPos.y + currentCameraHeight;

        transform.position = camera_pos;
    }

    public void Roll(float x, float y)
    {
        // �ړ��O�̋�����ۑ�����
        float prev_distance = Vector3.Distance(followObject.transform.position, transform.position);
        Vector3 pos = transform.position;

        // ���Ɉړ�����
        pos += transform.right * x;

        // �c�Ɉړ�����
        currentCameraHeight = Mathf.Clamp(currentCameraHeight + y, cameraHeightMin, cameraHeightMax);
        pos.y = lookPos.y + currentCameraHeight;

        // �ړ���̋������擾����
        float after_distance = Vector3.Distance(followObject.transform.position, pos);

        // ���_��ΏۂɌ����ċ߂Â���i�V�т��Ȃ����j
        lookPos = Vector3.Lerp(lookPos, followObject.transform.position, 0.1f);

        // �J�����̍X�V
        transform.position = pos;
        transform.LookAt(lookPos);

        // ���s�ړ��ɂ��኱�������ς��̂ŕ␳����
        transform.position += transform.forward * (after_distance - prev_distance);
    }
}
