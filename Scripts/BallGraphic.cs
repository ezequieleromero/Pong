using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGraphic : MonoBehaviour
{
    float alpha = GlobalVariables.Alpha;

    public void Start()
    {
    
    }
    public void Update()
    {   
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = alpha;
        GetComponent<SpriteRenderer>().color = tmp;

        alpha -= 0.05f;

        if(alpha < 0)
        {
            Destruction();
        }
    }

    public void Destruction()
    {
        Destroy(this.gameObject);
    }
}
