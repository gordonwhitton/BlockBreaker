using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameSession : MonoBehaviour
{
    // configuration parameters
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f; //give a minimum and maximum speed
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] bool isAutoPlayEnabled;

    // state variables
    [SerializeField] int currentScore = 0;

    //this is our singleton method
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length; //note difference - objectS of type this time 

        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false); //do this every time we do a singleton pattern

            //if already more than one
            Destroy(gameObject); //destroy yourself
        } else
        {
            DontDestroyOnLoad(gameObject); //therefore the game status from the previous level will hang around
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed; //start in regular time
    }

    internal bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject); //destroy yourself
    }
}
