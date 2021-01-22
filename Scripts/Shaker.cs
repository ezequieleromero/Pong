using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public ScreenShake Shake;
    public float duration;

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.collision == true)
        {
            Shake.Shake(duration);
            GlobalVariables.collision = false;
        }
    }
}
