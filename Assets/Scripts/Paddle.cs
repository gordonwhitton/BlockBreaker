using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] float minX = 1f; //because of width of paddle
    [SerializeField] float maxX = 15f;

    // cache references
    private GameSession gameSession;
    private Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
}

    [SerializeField] float screenWidthInUnits = 16;  // there should be 16 units (we are 8 on the x axis, so double

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mousePosition.x / Screen.width * screenWidthInUnits); //only want mouse position for X axis, divide by Screen width

        // this should give a value from 0 to 1 ( a %)

        // Vector2 paddlePosition = new Vector2(10f, 4f);

        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y); //starting position

        paddlePosition.x = Mathf.Clamp(GetXPos(), minX, maxX);


        transform.position = paddlePosition; //say go to this position
    }

    private float GetXPos()
    {
        
        if (gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
