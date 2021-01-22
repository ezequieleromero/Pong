using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //public GameObject Ball_Graphic;
    public GameObject Ball_Graphic;

    float init_speed = 5f;

    float speed;

    float radius;
    Vector2 direction;

    float distanceFromCamera = 10f;

    float speedLimit = 0f;

    float red = 255f;
    float green = 0f;
    float blue = 0f;

    float colorSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 1;

        //Ball_Graphic graphic1 = Instantiate(graphic) as Ball_Graphic;
        if (Difficulty.difficulty == 0)
        {
            speed = 5f;
        }
        else if (Difficulty.difficulty == 1)
        {
            speed = 7f;
        }
        else if (Difficulty.difficulty == 2)
        {
            speed = 9f;
        }
        else if (Difficulty.difficulty == 3)
        {
            speed = 5f;
        }

        direction = new Vector2(1, Random.Range(-1f, 1f));
        radius = transform.localScale.x / 2;

        Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));
        transform.position = centerPos;
    }

    // Update is called once per frame
    void Update()
    {

        if(blue < 255 && red == 255 && green <= 0)
        {
            blue += colorSpeed;
            green = 0;
        }
        else if (blue >= 255 && red > 0 && green == 0)
        {
            red -= colorSpeed;
            blue = 255;
        }
        else if (blue == 255 && red <= 0 && green < 255)
        {
            green += colorSpeed;
            red = 0;
        }
        else if (blue > 0 && red == 0 && green >= 255)
        {
            blue -= colorSpeed;
            green = 255;
        }
        else if (blue <= 0 && red < 255 && green == 255)
        {
            red += colorSpeed;
            blue = 0;
        }
        else if (blue == 0 && red >= 255 && green > 0)
        {
            green -= colorSpeed;
            red = 255;
        }

        //Paddle paddle1 = Instantiate(paddle) as Paddle;
        GameObject graphic1 = Instantiate(Ball_Graphic, transform.position, transform.rotation) as GameObject;

        graphic1.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        graphic1.GetComponent<SpriteRenderer>().sortingOrder = 0;
        graphic1.GetComponent<SpriteRenderer>().color = new Color(red/255f, green/255f, blue/255f);

        GlobalVariables.Alpha = speed/(init_speed*2);

        transform.Translate(direction * speed * Time.deltaTime);

        if(transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
        }
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        if (transform.position.x < (GameManager.bottomLeft.x - radius) && direction.x < 0)
        {
            if (Difficulty.difficulty == 3)
            {
                direction.x = -direction.x;
            }
            else
            {
                Debug.Log("Right Player Scores.");

                GlobalVariables.rightPoints += 1;

                if (GlobalVariables.rightPoints >= 11)
                {
                    Time.timeScale = 0;
                    enabled = false;
                }

                Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));
                transform.position = centerPos;

                direction = new Vector2(1, Random.Range(-1f, 1f));

                GlobalVariables.init_Intensity = 0.2f;
                GlobalVariables.Intensity = 0.2f;
            }
        }
        if (transform.position.x > (GameManager.topRight.x + radius) && direction.x > 0)
        {
            Debug.Log("Left Player Scores.");

            GlobalVariables.leftPoints += 1;

            if (GlobalVariables.leftPoints >= 11)
            {
                Time.timeScale = 0;
                enabled = false;
            }
            
            Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));
            transform.position = centerPos;

            direction = new Vector2(-1, Random.Range(-1f, 1f));

            GlobalVariables.init_Intensity = 0.2f;
            GlobalVariables.Intensity = 0.2f;
        }

        //Vertical Speed Limit

        speedLimit = Mathf.Abs(direction.x);

        if(Mathf.Abs(direction.y) > speedLimit)
        {
            direction.y = Mathf.Sign(direction.y) * speedLimit;
        }

        GlobalVariables.yBall = transform.position.y;
        GlobalVariables.xBallSpeed = direction.x;
    }

    float move = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Paddle" &&
            (!(transform.position.x > (GameManager.topRight.x - radius) && direction.x > 0) &&
            !(transform.position.x < (GameManager.bottomLeft.x + radius) && direction.x < 0)))
        {
            GlobalVariables.collision = true;
            GlobalVariables.init_Intensity += 0.025f;

            bool isRight = other.GetComponent<Paddle>().isRight;
            float move_y = other.gameObject.GetComponent<Paddle>().move;

            direction.x = -direction.x;

            if (move_y != 0)
            {
                direction.y += move_y * Random.Range(5, 10);
            }
            else
            {
                direction.y += Random.Range(-0.2f, 0.2f);
            }

            if (direction.x > 0)
            {
                direction.x += 0.1f;
            }
            else
            {
                direction.x -= 0.1f;
            }
        }
    }
}
