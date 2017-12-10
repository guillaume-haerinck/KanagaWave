using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ShakeLeftIsland : MonoBehaviour
{
    
    Vector3 originalPosition;
    float shakeAmount;
    float shakeTime = 0f;

    private static SC_ShakeLeftIsland _instance;

    void Awake()
    {
        _instance = this;
        originalPosition = gameObject.transform.localPosition;
    }


    void Update()
    {
        if (shakeTime > 0f)
        {
            float y = Random.Range(-shakeAmount, 0f);
            gameObject.transform.localPosition = originalPosition + new Vector3(0f, y, 0f);
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0f;
            gameObject.transform.localPosition = originalPosition;
        }
    }

    public static void Shake(int force)
    {
        force--;
        if (force > 0)
        {
            float newShakeAmount = 0.05f + ((force * (0.4f - 0.05f)) / 8);
            _instance.shakeAmount = Mathf.Max(newShakeAmount, _instance.shakeAmount);

            float shakeDuration = 0.05f + ((force * (0.5f - 0.05f)) / 8);
            _instance.shakeTime = Mathf.Max(_instance.shakeTime, shakeDuration);

            if (_instance.shakeTime <= 0f)
            {
                _instance.originalPosition = _instance.gameObject.transform.localPosition;
            }
        }
    }
}
