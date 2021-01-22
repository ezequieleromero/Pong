using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    Transform _target;
    Vector3 _initialPos;

    void Start()
    {
        _target = GetComponent<Transform>();
        _initialPos = _target.localPosition;
    }

    float _pendingShakeDuration = 0f;

    public void Shake(float duration)
    {
        if(duration > 0)
        {
            _pendingShakeDuration += duration;
        }    
    }

    bool _isShaking = false;

    void Update()
    {
        if(_pendingShakeDuration > 0 && !_isShaking)
        {
            StartCoroutine(DoShake());
        }
    }

    IEnumerator DoShake()
    {
        _isShaking = true;

        //var startTime = Time.realtimeSinceStartup;
        while(GlobalVariables.Intensity > 0)//Time.realtimeSinceStartup < startTime + _pendingShakeDuration)
        {
            var randomPoint = new Vector3(Random.Range(-1f, 1f) * GlobalVariables.Intensity, Random.Range(-1f, 1f) * GlobalVariables.Intensity, _initialPos.z);
            _target.localPosition = randomPoint;
            yield return null;

            GlobalVariables.Intensity *= 0.80f;

            if(GlobalVariables.Intensity < GlobalVariables.init_Intensity/100)
            {
                GlobalVariables.Intensity = 0;
            }
        }

        GlobalVariables.Intensity = GlobalVariables.init_Intensity;
        _pendingShakeDuration = 0f;
        _target.localPosition = _initialPos;
        _isShaking = false;
    }
}
