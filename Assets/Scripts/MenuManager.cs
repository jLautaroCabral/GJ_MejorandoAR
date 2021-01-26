using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScore;
    public GameObject quizCanvas;

    public static MenuManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    private void Start()
    {
        ActualizarHighScore();
    }

    public void ShowQuiz()
    {
        quizCanvas.SetActive(true);
    }

    public void HideQuiz()
    {
        quizCanvas.SetActive(false);
    }
    
    public void ActualizarTextMonedas()
    {
        scoreText.text = "Semillas: " + GameManager.sharedInstance.semillas;
    }

    public void ActualizarHighScore()
    {
        if (SavedData.sharedInstance.score != 0)
            return;
        
        highScore.text = "HighScore: " + SavedData.sharedInstance.highScore;
    }
}
