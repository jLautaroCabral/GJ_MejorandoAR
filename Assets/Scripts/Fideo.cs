using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fideo : MonoBehaviour
{
    /*
     * Este script se usa únicamente en la escena de preparar equipos para el Quiz en equipos.
     * Fue creado 9 horas antes del tiempo límite para entrega.
     * Lo llame Fideo porque su única función es ser una solución simple para ese problema muy particular,
     * que quizas requiere un manager más complejo para manener una mejor estructura. Pero me queda poco
     * tiempo y todavía no hice el GDD!!!
     *
     * En fin, es un chiste
     *
     * - Lautaro
     */

    [SerializeField] private Text textCantEquipos = null;
    [SerializeField] private Text textPuntosParaGanar = null;

    private int puntosParaGanar = 3;
    private int cantEquipos = 2;

    private int minPuntosParaGanar = 3;
    private int maxPuntosParaGanar = 10;
    private int minCantEquipos = 2;
    private int maxCantEquipos = 6;
    
    void Start()
    {
        actualizarTextos();
    }

    public void aumentarEquipo()
    {
        cantEquipos++;
        if (cantEquipos >= maxCantEquipos)
        {
            cantEquipos = maxCantEquipos;
        }

        SavedData.sharedInstance.cantidadEquipos = cantEquipos;
        actualizarTextos();
    }

    public void aumentarPuntosParaGanar()
    {
        puntosParaGanar++;
        if (puntosParaGanar >= maxPuntosParaGanar)
        {
            puntosParaGanar = maxPuntosParaGanar;
        }

        SavedData.sharedInstance.puntosParaGanar = puntosParaGanar;
        actualizarTextos();
    }

    public void reducirPuntosParaGanar()
    {
        puntosParaGanar--;
        if(puntosParaGanar <= minPuntosParaGanar)
        {
            puntosParaGanar = minPuntosParaGanar;
        }
        
        SavedData.sharedInstance.puntosParaGanar = puntosParaGanar;
        actualizarTextos();
    }

    public void reducirCantEquipos()
    {
        cantEquipos--;
        if(cantEquipos <= minCantEquipos)
        {
            cantEquipos = minCantEquipos;
        }
        
        SavedData.sharedInstance.cantidadEquipos = cantEquipos;
        actualizarTextos();
    }

    private void actualizarTextos()
    {
        textCantEquipos.text = "" + SavedData.sharedInstance.cantidadEquipos;
        textPuntosParaGanar.text = "" + SavedData.sharedInstance.puntosParaGanar;
    }
}
