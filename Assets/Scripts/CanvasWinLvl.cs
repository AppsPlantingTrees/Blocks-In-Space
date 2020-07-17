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
        "Earth by <link=\"https://unsplash.com/@nasa\"><br><u>NASA</u></link>", 
        "Milky Way by <link=\"https://unsplash.com/@jeremythomasphoto\"><br><u>Jeremy Thomas</u></link>", 
        "Andromeda Galaxy by <link=\"https://unsplash.com/@gferla\"><br><u>Guillermo Ferla</u></link>",
        "Moon by <link=\"https://unsplash.com/@gferla\"><br><u>Guillermo Ferla</u></link>", 
        "Carina Nebula by <link=\"https://unsplash.com/@nasa\"><br><u>NASA</u></link>"};


    public void SetUpCanvas(int currentLvl)
    {
        int coinsThisLevel = canvasGameInfo.GetComponent<CanvasGameInfo>().getCoinsThisLevel();
        int scoreThisLevel = canvasGameInfo.GetComponent<CanvasGameInfo>().getScoreThisLevel();
        buttonX2ForAd.SetActive(true);
        textCoins.GetComponent<Text>().text = "COINS: " + coinsThisLevel;
        textScore.GetComponent<Text>().text = "SCORE: " + scoreThisLevel;
        textPhoto.GetComponent<TextMeshProUGUI>().text = "Photo of the " + photoAutor[currentLvl-1];
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
