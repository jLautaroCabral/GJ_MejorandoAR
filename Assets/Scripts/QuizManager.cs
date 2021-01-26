using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    private bool jugadorNoRespondio;
    private int _segundos = 0;
    private int intentosIncorrectos = 0;
    private int intentosCorrectos = 0;
    private int intentos = 0;
    
    private int indiceQuizActual;
    
    [SerializeField] private Text textPregunta = null;
    [SerializeField] private string[] quizActual = new string[6];
    [SerializeField] private Text[] textOpciones = new Text[4];
    [SerializeField] private Image[] imageBotones = new Image[4];
    [SerializeField] private List<string[]> preguntas = null;
    [SerializeField] private AudioSource audioRespondioIncorrectamente = null;
    [SerializeField] private AudioSource audioRespondioCorrectamente = null;
    [SerializeField] private GameObject pantallaJugadosPerdioElQuiz = null;
    
    public int temporizador = 10;
    public string[][] listaPreguntas;
    
    public static QuizManager sharedInstance;
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }

        jugadorNoRespondio = true;
    }
    
    
    // Private:
    private void prepararListaPreguntas()
    {
        preguntas = listaPreguntas.ToList();
    }
    
    private void prepararTextRespuestas()
    {
        for (int i = 0; i < textOpciones.Length; i++)
        {
            textOpciones[i].text = quizActual[1 + i];
        }
    }
    
    private void prepararTextPregunta()
    {
        textPregunta.text = quizActual[0];
    }
    
    private void cambiarPregunta()
    {
        if(preguntas.Contains(quizActual))
        {
            preguntas.Remove(preguntas.ElementAt(indiceQuizActual));
        }

        if (preguntas.Count <= 0)
        {
            ManejarSeAcabaronLasPreguntas();
        }
        else
        {
            indiceQuizActual = Random.Range(0, preguntas.Count);
            quizActual = preguntas.ElementAt(indiceQuizActual);
        
            prepararTextPregunta();
            prepararTextRespuestas();
        }
    }
    
    private void resetearColorBotones()
    {
        for (int i = 0; i < imageBotones.Length; i++)
        {
            imageBotones[i].color = Color.white;
        }
    }

    private void elJugadorGanoElQuiz()
    {
        int actualScore = GameManager.sharedInstance.semillas;
        SavedData.sharedInstance.score = actualScore;

        if (actualScore > SavedData.sharedInstance.highScore)
        {
            SavedData.sharedInstance.highScore = actualScore;
        }
        
        GeneralSeceneManager.sharedInstance.CargarRunner();
    }

    private void elJugadorPerdioElQuiz()
    {
        int actualScore = GameManager.sharedInstance.semillas;
        SavedData.sharedInstance.score = 0;

        if (actualScore >= SavedData.sharedInstance.highScore)
        {
            SavedData.sharedInstance.highScore = actualScore;
        }
        
        pantallaJugadosPerdioElQuiz.SetActive(true);
    }
    
    #region Manejadores
    private void ManejarSeAcabaronLasPreguntas()
    {
        Debug.Log("Por un demonio! Lo que faltaba");
        GeneralSeceneManager.sharedInstance.CargarMenuPrincipal();
    }
    
    private void ManejarRespuestaCorrecta(int i)
    {
        intentosCorrectos++;
        imageBotones[i - 1].color = Color.green;
        audioRespondioCorrectamente.Play();
        jugadorNoRespondio = false;
        startTimer();
    }
    
    private void ManejarRespuestaIncorrecta(int i)
    {
        intentosIncorrectos++;
        imageBotones[i - 1].color = Color.red;
        audioRespondioIncorrectamente.Play();
        jugadorNoRespondio = false;
        startTimer();
    }

    private void ManejarTerminoElTimer()
    {
        if (intentosCorrectos >= 2)
        {
            elJugadorGanoElQuiz();
            return;
        } else if (intentosIncorrectos >= 2 || intentos >= 3)
        {
            elJugadorPerdioElQuiz();
            return;
        }
            
        restartTimer();
        jugadorNoRespondio = true;
        resetearColorBotones();
        cambiarPregunta();
    }
    #endregion

    #region TimerAlResponder
    private void startTimer()
    {
        Invoke(nameof(updateTimer), 1f);
    }
    private void restartTimer()
    {
        _segundos = -1;
    }
    private void updateTimer()
    {
        _segundos++;
        if (_segundos <= temporizador)
        {
            Invoke(nameof(updateTimer), 1f);
        }
        else
        {
            ManejarTerminoElTimer();
        }
    }
    #endregion

    // Public:
    public void ElegirRespuesta(int ind)
    {
        if (jugadorNoRespondio)
        {
            intentos++;
            if(ind.ToString() == quizActual[5])
            {
                ManejarRespuestaCorrecta(ind);
            }
            else
            {
                ManejarRespuestaIncorrecta(ind);
            }
        }
    }
    
    public void prepararQuiz()
    {
        prepararListaPreguntas();
        
        indiceQuizActual = 0;
        quizActual = preguntas.ElementAt(indiceQuizActual);

        prepararTextPregunta();
        prepararTextRespuestas();
    }
}
