using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizTeamManager : MonoBehaviour
{
    private bool jugadorNoRespondio;
    private int _segundos = 0;
    public int temporizador = 10;
    public int cantidadDeEquipos = 6;
    public int puntosParaGanar = 5;

    public string[][] listaPreguntas;
    
    private int indiceQuizActual;
    
    [SerializeField] private Text textPregunta = null;
    [SerializeField] private string[] quizActual = new string[6];
    [SerializeField] private Text[] textOpciones = new Text[4];
    [SerializeField] private GameObject[] puntosEquiposGmObj = new GameObject[6];
    [SerializeField] private Text[] puntosEquiposText = new Text[6];
    [SerializeField] private int[] puntosEquipos = new int[6] {0,0,0,0,0,0};
    [SerializeField] private Image[] imageBotones = new Image[4];
    
    [SerializeField] private List<string[]> preguntas = null;
    
    [SerializeField] private GameObject panelSeAcabaronLasPreguntas = null;
    [SerializeField] private GameObject panelUnEquipoGano = null;
    [SerializeField] private Text textPanelUnEquipoGano = null;
    [SerializeField] private Text textPanelSeAcabaronLasPreguntas = null;
    
    public AudioSource audioNegative;
    public AudioSource audioPositive;

    public static QuizTeamManager sharedInstance;
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    void Start()
    {
        jugadorNoRespondio = true;
        cantidadDeEquipos = SavedData.sharedInstance.cantidadEquipos;
        puntosParaGanar = SavedData.sharedInstance.puntosParaGanar;
            
        QuizTeamResourceManager.sharedInstance.cargarRecursosEnQuizTeamManager();
    }

    public void prepararQuiz()
    {
        prepararListaPreguntas();
        
        indiceQuizActual = 0;
        quizActual = preguntas.ElementAt(indiceQuizActual);

        prepararEquipos();
        prepararTextPregunta();
        prepararTextRespuestas();
    }
    
    // Private:
    private void prepararListaPreguntas()
    {
        preguntas = listaPreguntas.ToList();
    }
    
    private void prepararEquipos()
    {
        for (int i = cantidadDeEquipos; i < 6; i++ )
        {
            puntosEquiposGmObj[i].SetActive(false);
        }
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

    private void ManejarUnEquipoGano(int ind)
    {
        textPanelUnEquipoGano.text = "El ganador es el Equipo " + ind + "!";
        panelUnEquipoGano.SetActive(true);
    }
    
    private void ManejarSeAcabaronLasPreguntas()
    {
        int[] suppPuntosEquipos = new int[puntosEquipos.Length];
        puntosEquipos.CopyTo(suppPuntosEquipos, 0);
        
        Array.Sort(suppPuntosEquipos);

        if (suppPuntosEquipos[suppPuntosEquipos.Length - 1] == suppPuntosEquipos[suppPuntosEquipos.Length - 2])
        {
            textPanelSeAcabaronLasPreguntas.text = "Hay un empate!";
            panelSeAcabaronLasPreguntas.SetActive(true);
        }
        else
        {
            textPanelSeAcabaronLasPreguntas.text = "El ganador es el Equipo " + ( puntosEquipos.ToList().IndexOf(suppPuntosEquipos[suppPuntosEquipos.Length - 1]) + 1) + "!";
            panelSeAcabaronLasPreguntas.SetActive(true);
        }
        
        
        for (int i = 0; i < puntosEquipos.Length; i++)
        {
            Debug.Log(puntosEquipos[i]);
        }
    }
    
    private void ManejarRespuestaCorrecta(int i)
    {
        Debug.Log("Correcto");
        imageBotones[i - 1].color = Color.green;
        jugadorNoRespondio = false;
        
        audioPositive.Play();
        startTimer();
    }

    private void ManejarRespuestaIncorrecta(int i)
    {
        Debug.Log("Incorrecto, la respuesta correcta es " + quizActual[ Int32.Parse(quizActual[5]) ]);
        imageBotones[i - 1].color = Color.red;
        jugadorNoRespondio = false;
        
        audioNegative.Play();
        startTimer();
    }
    
    #region Timer
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
            Debug.Log(_segundos);
            Invoke(nameof(updateTimer), 1f);
        }
        else
        {
            restartTimer();
            jugadorNoRespondio = true;
            resetearColorBotones();
            cambiarPregunta();
        }
    }
    #endregion
    
    // Public:
    public void ElegirRespuesta(int ind)
    {
        if (jugadorNoRespondio)
        {
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
    
    public void AgregarPunto(int ind)
    {
        puntosEquipos[ind]++;
        puntosEquiposText[ind].text = "" + puntosEquipos[ind];

        if (puntosEquipos[ind] >= puntosParaGanar)
        {
            ManejarUnEquipoGano(ind);
        }
    }
    
    public void EliminarPunto(int ind)
    {
        if (puntosEquipos[ind] > 0)
        {
            puntosEquipos[ind]--;
            puntosEquiposText[ind].text = "" + puntosEquipos[ind];
        }
    }
}
