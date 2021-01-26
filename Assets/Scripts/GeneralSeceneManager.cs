using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralSeceneManager : MonoBehaviour
{
    private const string MENU = "menuScene";

    private const string QUIZ_TEAM = "quizTeamScene";

    private const string RUNNER = "runnerScene";

    private const string TEAM_SELECTION = "quizTeamSelectorScene";

    public static GeneralSeceneManager sharedInstance = null;
    
    void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void CargarMenuPrincipal()
    {
        SceneManager.LoadScene(MENU);
    }

    public void CargarRunner()
    {
        SceneManager.LoadScene(RUNNER);
    }

    public void CargarQuizTeam()
    {
        SceneManager.LoadScene(QUIZ_TEAM);
    }

    public void CargarSeleccionDeEquipos()
    {
        SceneManager.LoadScene(TEAM_SELECTION);
    }
}
