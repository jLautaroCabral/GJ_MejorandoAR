using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] obj;
    public float tiempoMin = 1.25f;
    public float tiempoMax = 2.5f;
    private bool fin = false;
    
    public void GenerateOff(){
        fin = true;
    }

    public void GenerateOn(){
        fin = false;
        Generar();
    }

    void Generar(){
        if(!fin){
            Instantiate(obj[Random.Range(0,obj.Length)], transform.position, Quaternion.identity);
            Invoke("Generar", Random.Range(tiempoMin, tiempoMax));
        }
    }
}
