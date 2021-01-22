using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    
    float height;

    string input;
    public bool isRight;

    public float move = 0;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
    }

    // Update is called once per frame
    public void Init(bool isRightPaddle)
    {
        isRight = isRightPaddle;

        Vector2 pos = Vector2.zero;

        if (isRightPaddle)
        {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;

            input = "PaddleRight";

            if (GlobalVariables.AI == true)
            {
                if (Difficulty.difficulty == 0)
                {
                    speed = 3f;
                }
                else if (Difficulty.difficulty == 1)
                {
                    speed = 7f;
                }
                else if (Difficulty.difficulty == 2)
                {
                    speed = 12f;
                }
                else if (Difficulty.difficulty == 3)
                {
                    speed = 12f;
                }
            }
            else
            {
                speed = 20f;
            }
        }
        else
        {
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;

            input = "PaddleLeft";
        }

        transform.position = pos;

        transform.name = input;
    }

    void Update()
    {
        if (GlobalVariables.AI == false || isRight == false)
        {
            move = Input.GetAxis(input) * Time.deltaTime * speed;
        }
        else
        {
            if (GlobalVariables.xBallSpeed > 0)
            {
                if (transform.position.y < GlobalVariables.yBall)
                {
                    move = 1 * Time.deltaTime * speed;
                }
                else
                {
                    move = -1 * Time.deltaTime * speed;
                }
            }
            else
            {
                move = 0;
            }
        }

        if ((transform.position.y < GameManager.bottomLeft.y + height / 1.7) && move < 0)
        {
            move = 0;
            Vector3 centerPos = new Vector3(transform.position.x, GameManager.bottomLeft.y + (height / 1.71f), 10f);
            transform.position = centerPos;
        }
        else if ((transform.position.y > GameManager.topRight.y - height / 1.7) && move > 0)
        {
            move = 0;
            Vector3 centerPos = new Vector3(transform.position.x, GameManager.topRight.y - (height / 1.71f), 10f);
            transform.position = centerPos;
        }
        else
        {
            transform.Translate(move * Vector2.up);
        }
    }
}
