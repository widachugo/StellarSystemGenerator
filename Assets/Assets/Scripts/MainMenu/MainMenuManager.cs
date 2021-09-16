using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Object systemSolarGenerator;

    private int nbDifficultyChoice = 2;
    private int nbPlanet = 4;

    public Text textDifficulty;
    public Text textNbPlanet;

    public void Start()
    {
        Difficulty();
    }

    public void Difficulty()
    {
        if (nbDifficultyChoice == 1)
        {
            textDifficulty.text = "Facile".ToString();
        }
        if (nbDifficultyChoice == 2)
        {
            textDifficulty.text = "Normal".ToString();
        }
        if (nbDifficultyChoice == 3)
        {
            textDifficulty.text = "Difficile".ToString();
        }
    }

    public void DifficultyChoice()
    {
        if (nbDifficultyChoice != 3 )
        {
            nbDifficultyChoice++;
            Difficulty();
        } 
        else
        {
            nbDifficultyChoice = 1;
            Difficulty();
        }
    }

    public void ChangeNbPlanet()
    {
        if (nbPlanet != 10)
        {
            nbPlanet++;
            ChangeTextNbPlanet();
        }
        else
        {
            nbPlanet = 4;
            ChangeTextNbPlanet();
        }
    }

    public void ChangeTextNbPlanet()
    {
        textNbPlanet.text = nbPlanet.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
