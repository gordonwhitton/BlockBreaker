using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class losecolider : MonoBehaviour
{
    private const string GameOver = "GameOver";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(GameOver); //pass in the name of the scene we created in Unity
    }
}
