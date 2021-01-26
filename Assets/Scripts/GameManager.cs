using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    inGame,
    gameOver,
    other
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.other;
    public static GameManager sharedInstance;
    private PlayerController playerController;
    private AudioSource audioSMusicaGeneral;
    
    public int semillas = 0;

    public GameObject canvas = null;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioSMusicaGeneral = GetComponent<AudioSource>();
    }

    void Start()
    {
        semillas = SavedData.sharedInstance.score;
        MenuManager.sharedInstance.ActualizarTextMonedas();
        StartGame();
    }
    
    public void StartGame()
    {
        Debug.Log("Start!");
        if (currentGameState != GameState.inGame)
        {
            SetGameState(GameState.inGame);
        }
    }
    
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }
    

    private void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.inGame:
                ManejarInGameState();
                break;
            case GameState.gameOver:
                ManejarGameOverState();
                break;
        }
        
        this.currentGameState = newGameState;
    }

    void ManejarInGameState()
    {
        audioSMusicaGeneral.Play();
        GeneratorManager.sharedInstance.Generar();
        playerController.StartGame();
    }

    void ManejarGameOverState()
    {
        audioSMusicaGeneral.Stop();
        GeneratorManager.sharedInstance.DejarDeGenerar();
        QuizResourceManager.sharedInstance.cargarRecursosEnQuizManager();
        MenuManager.sharedInstance.ShowQuiz();
    }

    public void AumentarMonedas()
    {
        semillas++;
        MenuManager.sharedInstance.ActualizarTextMonedas();
    }

    public void ReacargarEscena()
    {
        SceneManager.LoadScene("Runner");
    }
}
