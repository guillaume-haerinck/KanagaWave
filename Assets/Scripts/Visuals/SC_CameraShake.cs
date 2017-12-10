using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CameraShake : MonoBehaviour
{

    Vector3 originalPosition;
    float shakeAmount;
    float shakeTime = 0f;

    private static SC_CameraShake _instance;

    void Awake()
    {
        _instance = this;
        originalPosition = gameObject.transform.localPosition;
    }


    void Update()
    {
        if (shakeTime > 0f)
        {
            gameObject.transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0f;
            gameObject.transform.localPosition = originalPosition;
        }
    }

    public static void Shake(float _f_ShakeDuration = 0.2f, float _f_ShakeAmount = 0.4f)
    {
        _instance.shakeAmount = Mathf.Max(_instance.shakeAmount, _f_ShakeAmount);
        _instance.shakeTime = Mathf.Max(_instance.shakeTime, _f_ShakeDuration);

        if (_instance.shakeTime <= 0f)
        {
            _instance.originalPosition = _instance.gameObject.transform.localPosition;
        }
    }
}
