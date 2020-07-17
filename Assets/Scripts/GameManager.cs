using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

/*
Main object, manage game state, menus and screens,
count levels and broken blocks.
*/

public class GameManager : MonoBehaviour
{
    public GameObject winScreen, loseScreen, winGameScreen, textGameScore;
    public GameObject mainMenu, pauseMenu, canvasDarkenScreen, canvasGameInfo, canvasBlocks;
    public GameObject aboutMenu, upgradesMenu; //not initialized
    public Ball ball;
    public BallPlasma ballPlasma;
    public Racket racket;
    Canvas blocks;

    float barrierDuration, plasmaBallDuration;
    private int currentCounter = 0, currentLvl;
    public int winCounter = 100;
    bool isAboutMenuInstatillated = false, isUpgradesMenuInstatillated = false;

    private const int MAX_LVL = 5;


    void Start()
    {
      //PlayerPrefs.SetInt("CurrentLvl", 1); //for test
      //saveDataForTest();
      currentLvl = PlayerPrefs.GetInt("CurrentLvl", 1);
      canvasGameInfo.GetComponent<CanvasGameInfo>().UpdateLvl(currentLvl); 
      canvasGameInfo.GetComponent<CanvasGameInfo>().UpdateAllFromPrefs();
       
      //if there is a save - try to load it
      //if no - install current lvl prefabs:
      if (! GetComponent<SaveLoadManager>().tryLoadObjectsData(currentLvl))
      {
        StartNewLvl();
      }

      GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
      winCounter = allBlocks.Length;
      winCounter = 0; //for test
    }

    public void StartNewLvl() 
    {
      Debug.Log("Start new lvl: " + currentLvl);
      CleanField();
      winScreen.SetActive(false);
      loseScreen.SetActive(false);
      winGameScreen.SetActive(false);
  
      blocks = canvasBlocks.GetComponent<CanvasBlocks>().SetUpBlocks(currentLvl); 
      GetComponent<SetBackground>().SetUpBackground(currentLvl);
      Instantiate(racket, new Vector2(0, -95), Quaternion.identity);
      Instantiate(ball, new Vector2(0, -85), Quaternion.identity);
      canvasGameInfo.GetComponent<CanvasGameInfo>().UpdateLvl(currentLvl); 
      canvasGameInfo.GetComponent<CanvasGameInfo>().setScoreThisLevelToNull(); 
      canvasGameInfo.GetComponent<CanvasGameInfo>().setCoinsThisLevelToNulL(); 

      currentCounter = 0;

      GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
      winCounter = allBlocks.Length;

      UndarkenScreenUnpauseTime();
      Debug.Log("winCounter: " + winCounter);
    }

    private void CleanField() 
    {
      GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
      foreach(GameObject ball in allBalls)
      {
        Destroy(ball);
      }

      GameObject[] allPBalls = GameObject.FindGameObjectsWithTag("PlasmaBall");
      foreach(GameObject pBall in allPBalls)
      {
        Destroy(pBall);
      }

      Destroy(GameObject.FindWithTag("Racket"));

      GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
      foreach(GameObject block in allBlocks)
      {
        Destroy(block);
      }

      GameObject[] allSteelBlocks = GameObject.FindGameObjectsWithTag("BlockSteel");
      foreach(GameObject blockSteel in allSteelBlocks)
      {
        Destroy(blockSteel);
      }

      GameObject[] allCoins = GameObject.FindGameObjectsWithTag("Coin");
      foreach(GameObject coin in allCoins)
      {
        Destroy(coin);
      }

      GameObject[] allBonuses = GameObject.FindGameObjectsWithTag("Bonus");
      foreach(GameObject bonus in allBonuses)
      {
        Destroy(bonus);
      }

      GameObject[] allTelIn = GameObject.FindGameObjectsWithTag("TeleportIn");
      foreach(GameObject telIn in allTelIn)
      {
        Destroy(telIn);
      }

      GameObject[] allTelOut = GameObject.FindGameObjectsWithTag("TeleportOut");
      foreach(GameObject telOut in allTelOut)
      {
        Destroy(telOut);
      }
    }

    public void CountBlock()
    {
      currentCounter++;
      if (currentCounter >= winCounter)
      {
        Win();
      }
    }

    public void Win()
    {
      DarkenScreenPauseTime();
      if (currentLvl < MAX_LVL)
      {
        winScreen.SetActive(true);
        winScreen.GetComponent<CanvasWinLvl>().SetUpCanvas(currentLvl);        
      } else {
        //it was last lvl, you win the game:
        winGameScreen.SetActive(true);
        int score = canvasGameInfo.GetComponent<CanvasGameInfo>().getScore();
        textGameScore.GetComponent<Text>().text = "SCORE: " + score;
      }
    }

    public void Lose()
    {
      DarkenScreenPauseTime();
      loseScreen.SetActive(true);
    }

    void DarkenScreenPauseTime()
    {
      Time.timeScale = 0.0f;
      canvasDarkenScreen.SetActive(true);
    }

    void UndarkenScreenUnpauseTime()
    {
      canvasDarkenScreen.SetActive(false);
      Time.timeScale = 1.0f;
    }

    public void NewGame()
    {
      //todo - ?
      PlayerPrefs.SetInt("Score", 0);
      PlayerPrefs.SetInt("Balls", 3);
      PlayerPrefs.SetInt("CurrentLvl", 1);
      currentLvl = 1;

      canvasGameInfo.GetComponent<CanvasGameInfo>().UpdateAllFromPrefs();      
      StartNewLvl();
    }

    public void NextLevel()
    {
      currentLvl++;
      StartNewLvl();
    }

    public void ShowMainMenu()
    {
      DarkenScreenPauseTime();
      //todo - you can just get data with getter/args!
      canvasGameInfo.GetComponent<CanvasGameInfo>().saveData();
      mainMenu.SetActive(true);
    }

    public void HideMainMenu()
    {
      mainMenu.SetActive(false);
      //todo - use setter! don't need to use prefs here!
      canvasGameInfo.GetComponent<CanvasGameInfo>().loadData();
      UndarkenScreenUnpauseTime();
    }

    public void ShowPauseMenu()
    {
      DarkenScreenPauseTime();
      pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
      pauseMenu.SetActive(false);
      UndarkenScreenUnpauseTime();
    }

    public void OpenOrShowAboutMenu()
    {
      DarkenScreenPauseTime();
      if (isAboutMenuInstatillated) {
        aboutMenu.SetActive(true);
      } else {
        Instantiate(aboutMenu, new Vector2(0, 0), Quaternion.identity);
        isAboutMenuInstatillated = true;
      }
    }

    public void OpenOrShowUpgradesMenu()
    {
      if (isUpgradesMenuInstatillated) {
        upgradesMenu.SetActive(true);
      } else {
        Instantiate(upgradesMenu, new Vector2(0, 0), Quaternion.identity);
        isUpgradesMenuInstatillated = true;
      }
    }

    void saveDataForTest()
    {
      PlayerPrefs.SetInt("Coins", 1000);
      PlayerPrefs.SetInt("Balls", 3);

      PlayerPrefs.SetFloat("BarrierDuration", 5f);
      PlayerPrefs.SetFloat("PlasmaBallDuration", 5f);
      PlayerPrefs.SetInt("PriceLife", 25);
      PlayerPrefs.SetInt("PriceBarrier", 25);
      PlayerPrefs.SetInt("PricePlasma", 25);
    }
}
