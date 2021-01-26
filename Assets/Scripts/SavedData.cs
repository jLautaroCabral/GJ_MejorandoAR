using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    public static SavedData sharedInstance = null;
    // Runner
    public int highScore = 0;
    public int score = 0;
    // QuizTeam
    public int cantidadEquipos = 2;
    public int puntosParaGanar = 5;
    
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        } else if (sharedInstance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
