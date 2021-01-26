using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    public static GeneratorManager sharedInstance = null;
    [SerializeField] private Generator[] generadores = null;
    private void Start()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void Generar()
    {
        for (int i = 0; i < generadores.Length; i++)
        {
            generadores[i].GenerateOn();
        }
    }

    public void DejarDeGenerar()
    {
        for (int i = 0; i < generadores.Length; i++)
        {
            generadores[i].GenerateOff();
        }
    }

    public void EliminarBloquesGenerados()
    {
        GameObject[] bloquesGenerados = GameObject.FindGameObjectsWithTag("BloqueGen");

        for (int i = 0; i < bloquesGenerados.Length; i++)
        {
            Debug.Log("Eliminando bloque " + i);
            Destroy(bloquesGenerados[i]);
        }
    }
}
