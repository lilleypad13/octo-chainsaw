using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame ()
    {
        SceneManager.LoadScene(2);
    }
    
    public void QuitGame ()
    {
        Debug.Log ("QUIT!");
        Application.Quit();

    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToAlpha()
    {
        SceneManager.LoadScene(8);
    }
}
