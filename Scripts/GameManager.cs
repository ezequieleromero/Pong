using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static int leftPoints = 0;
    public static int rightPoints = 0;
    public static bool collision = false;

    //Ball position
    public static float yBall = 0f;
    public static float xBallSpeed = 0f;
    public static float Alpha = 0.0f;

    //Screen Shake
    public static float Intensity = 0.2f;
    public static float init_Intensity = 0.2f;

    //Game Mode
    public static bool AI = true;
}

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;

    public Font font;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Debug.Log(topRight);

        Instantiate(ball);
        Paddle paddle1 = Instantiate(paddle) as Paddle;
        Paddle paddle2 = Instantiate(paddle) as Paddle;

        paddle1.Init(true); //Right Paddle
        paddle2.Init(false); //Left Paddle
    }

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable

    void OnGUI()
    {
        guiStyle.font = font;
        guiStyle.fontSize = 60; //change the font size
        guiStyle.normal.textColor = Color.white;

        int width = 100;
        int height = 20;

        if (!font)
        {
            Debug.Log("No Font Found");
        }

        GUI.skin.font = font;
        if (Difficulty.difficulty == 0)
        {
            GUILayout.Label("EASY");
        }
        if (Difficulty.difficulty == 1)
        {
            GUILayout.Label("MEDIUM");
        }
        if (Difficulty.difficulty == 2)
        {
            GUILayout.Label("HARD");
        }
        if (Difficulty.difficulty == 3)
        {
            GUILayout.Label("BEESTON MODE");
        }
        GUI.Label(new Rect((Screen.width / 2) - (width / 2) - 100, (Screen.height / 2) - (height / 2) - 5, (Screen.width / 2) + (width / 2), (Screen.height / 2) + (height / 2)), GlobalVariables.leftPoints.ToString(), guiStyle);
        GUI.Label(new Rect((Screen.width / 2) - (width / 2) + 200, (Screen.height / 2) - (height / 2) - 5, (Screen.width / 2) + (width / 2), (Screen.height / 2) + (height / 2)), GlobalVariables.rightPoints.ToString(), guiStyle);

        if (GlobalVariables.rightPoints >= 11)
        {
            GUI.Label(new Rect((Screen.width / 2) - (width / 2) - 200, (Screen.height / 2) - (height / 2) - 80, (Screen.width / 2) + (width / 2), (Screen.height / 2) + (height / 2)), "RIGHT PLAYER WINS!", guiStyle);
        }
        else if (GlobalVariables.leftPoints >= 11)
        {
            GUI.Label(new Rect((Screen.width / 2) - (width / 2) - 200, (Screen.height / 2) - (height / 2) - 80, (Screen.width / 2) + (width / 2), (Screen.height / 2) + (height / 2)), "LEFT PLAYER WINS!", guiStyle);
        }
    }
}
