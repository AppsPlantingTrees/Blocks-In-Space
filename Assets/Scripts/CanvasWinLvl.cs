using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CanvasWinLvl : MonoBehaviour
{
    public GameObject canvasGameInfo;
    public GameObject textCoins, textScore, buttonX2ForAd;

    public void StartCanvas()
    {
        int coinsThisLevel = canvasGameInfo.GetComponent<CanvasGameInfo>().getCoinsThisLevel();
        int scoreThisLevel = canvasGameInfo.GetComponent<CanvasGameInfo>().getScoreThisLevel();
        buttonX2ForAd.SetActive(true);
        textCoins.GetComponent<Text>().text = "COINS: " + coinsThisLevel;
        textScore.GetComponent<Text>().text = "SCORE: " + scoreThisLevel;
    }

    public void x2CoinsForAd() 
    {
        int coinsThisLevel = canvasGameInfo.GetComponent<CanvasGameInfo>().getCoinsThisLevel();
        coinsThisLevel = 2 * coinsThisLevel;
        textCoins.GetComponent<Text>().text = "COINS: " + coinsThisLevel;
        //increase amount of coins on the screen and in the game:
        canvasGameInfo.GetComponent<CanvasGameInfo>().UpdateGold(coinsThisLevel);
        //disable button after 1 duplication (only 1 duplication/level allowed):
        buttonX2ForAd.SetActive(false);
    }
}
