using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/*
Count and shows on the screen number of balls, coins and score.
Saves/loads these data to/from prefs.
*/

public class CanvasGameInfo : MonoBehaviour
{
    public GameObject borderLeft, borderRight;
    public GameObject displayScore, displayBalls, displayCoins, displayLvl;
    private int balls, coins, score;
    private int scoreThisLevel = 0, coinsThisLevel = 0;

    private string[] lvlNames = {"1 | Earth", "2 | Milky Way", "3 | Andromeda galaxy",
            "4 | Moon", "5 | Carina Nebula", "6 | Orion Nebula", "7 | Sombrero galaxy", 
            "8 | Bubble Nebula", "9 | Protostar", "10 | Triangulum Galaxy",
             "11 | Pleiades", "12 | Milky Way", "13 | Eagle Nebula", "14 | Comet Neowise", 
             "15 | Solar Eclipse", "16 | Rosette Nebula", "17 | Eagle Nebula", "18 | Orion's belt", 
             "19 | Moon", "20 | Helix Nebula", "21 | Milky Way", "22 | Iris Nebula", "23 | Earth",
             "24 | Bubble Nebula", "25 | Needle Galaxy", };

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreThisLevel += scoreToAdd;
        displayScore.GetComponent<Text>().text = "" + score;
    }

    public void UpdateLives(int ballsUpdated)
    {
        balls = ballsUpdated;
        displayBalls.GetComponent<Text>().text = "" + ballsUpdated;
    }

    public void UpdateGold(int coinsToAdd)
    {
        coins += coinsToAdd;       
        displayCoins.GetComponent<Text>().text = "" + coins;
    }

    public void UpdateGoldCatchedCoin()
    {
        coins++;
        coinsThisLevel++;
        displayCoins.GetComponent<Text>().text = "" + coins;
    }

    public void UpdateLvl(int lvl)
    {
        displayLvl.GetComponent<Text>().text = "LVL: " + lvlNames[lvl-1].ToUpper();
    }

    public void StartGameInfo(int currentLvl)
    {
        loadData();
        UpdateLvl(currentLvl);
        UpdateGold(0);
        UpdateScore(0);
        UpdateLives(balls);
    }

    public int getScoreThisLevel() { return scoreThisLevel;}
    public int getCoinsThisLevel() { return coinsThisLevel;}
    public int getCoins() { return coins;}
    public int getScore() { return score;}

    public void setScoreThisLevelToNull() { scoreThisLevel = 0;}
    public void setCoinsThisLevelToNull() { coinsThisLevel = 0;}

    public bool LivesMinus()
    {
      balls--;
      UpdateLives(balls);
      if (balls < 1)
      {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Lose();
        return false;
      } else {
        return true;
      }
    }

    public void LivesPlus()
    {
      balls++;
      UpdateLives(balls);
    }

    public void saveData()
    {
      PlayerPrefs.SetInt("Coins", coins);
      PlayerPrefs.SetInt("Balls", balls);
      PlayerPrefs.SetInt("Score", score);
      PlayerPrefs.SetInt("CurrentCoins", coinsThisLevel);
      PlayerPrefs.SetInt("CurrentScore", scoreThisLevel);
    }

    public void loadData()
    {
      coins = PlayerPrefs.GetInt("Coins", 0);
      balls = PlayerPrefs.GetInt("Balls", 3);
      score = PlayerPrefs.GetInt("Score", 0);
      coinsThisLevel = PlayerPrefs.GetInt("CurrentCoins", 0);
      scoreThisLevel = PlayerPrefs.GetInt("CurrentScore", 0);
    }

}
