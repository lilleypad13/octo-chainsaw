﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    bool gameHasEnded;

    private int level;

    public int totalLevels; //Only necessary for CompleteLevel Script to determine if there are no more leves, and it should just restart the game
    public Text levelText;


    private void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        totalLevels -= 1; //Used to get the final build index number
        //levelText.text = "Level " + (level + 1);
        gameHasEnded = false;
    }

    public void WinGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("You have won!");
            SceneManager.LoadScene(2); // Will want to bring this back once the game is further along.
        }
    }

    public void CompleteLevel()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level + 1);
    }

    //public void CompleteLevel()
    //{
    //    if (gameHasEnded == false)
    //    {
    //        if (level >= totalLevels)     // Checks if the upcoming level would be greater than the total number of levels, so it can return the player to the beginning of the game.
    //        {
    //            gameHasEnded = true;
    //            level = 0;
    //            completeLevelUI.SetActive(true);
    //        }
    //        else // This just advances the player to the next level
    //        {
    //            level += 1;
    //            gameHasEnded = true;
    //            nextLevelUI.SetActive(true);
    //        }

    //    }
    //}

    public void LoseGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            SceneManager.LoadScene(1);
            Debug.Log("You have lost.");
        }
    }

    // Goes back to main menu now
    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(level);
        SceneManager.LoadScene(0);
        // Will want to replace this with the build index of our "Game Over" screen
        Debug.Log("Game has restarted.");
    }
}
