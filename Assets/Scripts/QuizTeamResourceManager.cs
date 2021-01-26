using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizTeamResourceManager : MonoBehaviour
{

    string[][] listaPreguntas = new string[9][];

    public static QuizTeamResourceManager sharedInstance = null;
    
    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    
    public void cargarRecursosEnQuizTeamManager()
    {
        for(int i = 0; i < listaPreguntas.Length; i++)
        {
            listaPreguntas[i] = new string[6];
        }
        
        listaPreguntas[0][0] = "¿Cuantos pies tiene una almeja?";
        listaPreguntas[0][1] = "Uno";
        listaPreguntas[0][2] = "Dos";
        listaPreguntas[0][3] = "Tres";
        listaPreguntas[0][4] = "Cuatro";
        listaPreguntas[0][5] = "1";
        
        listaPreguntas[1][0] = "¿Cuantos patas tiene una vaca?";
        listaPreguntas[1][1] = "1";
        listaPreguntas[1][2] = "2";
        listaPreguntas[1][3] = "3";
        listaPreguntas[1][4] = "4";
        listaPreguntas[1][5] = "4";
        
        listaPreguntas[2][0] = "¿Cuantos patas tiene un pato?";
        listaPreguntas[2][1] = "1";
        listaPreguntas[2][2] = "2";
        listaPreguntas[2][3] = "3";
        listaPreguntas[2][4] = "4";
        listaPreguntas[2][5] = "2";
        
        listaPreguntas[3][0] = "¿Cuál es la capital de San Luis?";
        listaPreguntas[3][1] = "La Punta";
        listaPreguntas[3][2] = "Santa Rosa";
        listaPreguntas[3][3] = "San Luis";
        listaPreguntas[3][4] = "Merlo";
        listaPreguntas[3][5] = "3";
        
        listaPreguntas[4][0] = "¿Cuanto es XIX en números romanos?";
        listaPreguntas[4][1] = "21";
        listaPreguntas[4][2] = "20";
        listaPreguntas[4][3] = "19";
        listaPreguntas[4][4] = "212";
        listaPreguntas[4][5] = "3";
        
        listaPreguntas[5][0] = "¿En qué provincia se encuentra el lago Traful?";
        listaPreguntas[5][1] = "Neuquén";
        listaPreguntas[5][2] = "Rio Negro";
        listaPreguntas[5][3] = "Santa Cruz";
        listaPreguntas[5][4] = "La Pampa";
        listaPreguntas[5][5] = "1";
        
        listaPreguntas[6][0] = "¿Cuántos volcanes activos hay en el país?";
        listaPreguntas[6][1] = "9";
        listaPreguntas[6][2] = "37";
        listaPreguntas[6][3] = "19";
        listaPreguntas[6][4] = "12";
        listaPreguntas[6][5] = "2";
        
        listaPreguntas[7][0] = "¿Cuál es el lago o laguna más grande del país?";
        listaPreguntas[7][1] = "Lago Argentina (Santa Cruz)";
        listaPreguntas[7][2] = "Lago Nahuel Huapi (Río Negro/Neuquén)";
        listaPreguntas[7][3] = "Lago San Roque (Córdoba)";
        listaPreguntas[7][4] = "Laguna Mar Chiquita (Córdoba)";
        listaPreguntas[7][5] = "4";
        
        listaPreguntas[8][0] = "¿Cuál es la lengua más hablada del mundo?";
        listaPreguntas[8][1] = "Español";
        listaPreguntas[8][2] = "Inglés";
        listaPreguntas[8][3] = "Islandés";
        listaPreguntas[8][4] = "Chino mandarín";
        listaPreguntas[8][5] = "4";
        
        QuizTeamManager.sharedInstance.listaPreguntas = this.listaPreguntas;
        QuizTeamManager.sharedInstance.prepararQuiz();
    }
}
