using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
    private Snake snake;

    private void Awake() 
    {
        snake = FindObjectOfType<Snake>();     
    }

    public void PayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void TryAgain()
    {
        snake.ResetState();
    }   

    public void Exit()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
