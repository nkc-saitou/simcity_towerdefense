using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeStatus
{
    noon,
    night
}

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    RectTransform TimeImage;

    float noonTime = 3.0f;
    float nightTime = 3.0f;

    float noonNowTime = 0.0f;
    float nightNowTime = 0.0f;

    float noonStartRotation = 0.0f;
    float noonEndRotation = 180.0f;

    float nightStartRotation = 180.0f;
    float nightEndRotation = 360.0f;

    TimeStatus nowTimeStatus = TimeStatus.noon;

    float noontime = 0.0f;
    float nighttime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nowTimeStatus == TimeStatus.noon)
        {
            noontime = Time.deltaTime / noonTime;
            noonNowTime += noonStartRotation + noontime * (noonEndRotation - noonStartRotation);

            TimeImage.rotation = Quaternion.Euler(TimeImage.rotation.x, TimeImage.rotation.y, noonNowTime);
        }
        else if (nowTimeStatus == TimeStatus.night)
        {
            nighttime = Time.deltaTime / nightTime;
            nightNowTime += (nightStartRotation + nighttime * (nightEndRotation - nightStartRotation)) + noonNowTime;
            Debug.Log(nightNowTime);

            TimeImage.rotation = Quaternion.Euler(TimeImage.rotation.x, TimeImage.rotation.y, nightNowTime);
        }


        if (noonNowTime >= noonEndRotation && nightNowTime < nightEndRotation && nowTimeStatus == TimeStatus.noon) nowTimeStatus = TimeStatus.night;
        /*
else if (noonNowTime >= nightEndRotation && nowTimeStatus == TimeStatus.night)
{
    Debug.Log(noonNowTime);
    noonNowTime = 0;
    nowTimeStatus = TimeStatus.noon;
}
*/


    }
}
