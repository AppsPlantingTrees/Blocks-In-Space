using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class CanvasWinLvl : MonoBehaviour
{
    public GameObject canvasGameInfo;
    public GameObject textCoins, textScore, textPhoto, buttonX2ForAd;

    private string[] photoAutor = {
        "<link=\"https://unsplash.com/@nasa\"><u>NASA</u></link>", 
        "<link=\"https://unsplash.com/@jeremythomasphoto\"><u>Jeremy Thomas</u></link>", 
        "<link=\"https://unsplash.com/@gferla\"><u>Guillermo Ferla</u></link>",
        "<link=\"https://unsplash.com/@gferla\"><u>Guillermo Ferla</u></link>", 
        "<link=\"https://unsplash.com/@nasa\"><u>NASA</u></link>",
        "<link=\"https://unsplash.com/@alex_andrews\"><u>Alexander Andrews</u></link> and <link=\"https://unsplash.com/@bryangoffphoto\"><u>Bryan Goff</u></link>",
        "<link=\"https://unsplash.com/@gferla\"><u>Guillermo Ferla</u></link>",
        "<link=\"https://unsplash.com/@nasa\"><u>NASA</u></link>",
        "<link=\"https://unsplash.com/@nasa\"><u>NASA</u></link>",
        "<link=\"https://unsplash.com/@gferla\"><u>Guillermo Ferla</u></link>",};


    public void SetUpCanvas(int currentLvl)
    {
        int coinsThisLevel = canvasGameInfo.GetComponent<CanvasGameInfo>().getCoinsThisLevel();
        int scoreThisLevel = canvasGameInfo.GetComponent<CanvasGameInfo>().getScoreThisLevel();
        buttonX2ForAd.SetActive(true);
        textCoins.GetComponent<Text>().text = "COINS: " + coinsThisLevel;
        textScore.GetComponent<Text>().text = "SCORE: " + (scoreThisLevel + 100);
        textPhoto.GetComponent<TextMeshProUGUI>().text = "Background based on photos<br> by " + photoAutor[currentLvl-1];
    }

    public void x2CoinsForAd() 
    {
        int coinsThisLevel = canvasGameInfo.GetComponent<CanvasGameInfo>().getCoinsThisLevel();
        textCoins.GetComponent<Text>().text = "COINS: " + 2 * coinsThisLevel;
        //increase amount of coins on the screen and in the game:
        canvasGameInfo.GetComponent<CanvasGameInfo>().UpdateGold(coinsThisLevel);
        //disable button after 1 duplication (only 1 duplication/level allowed):
        buttonX2ForAd.SetActive(false);
    }
}
