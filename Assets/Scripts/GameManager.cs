using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private static int currentCounter = 0, currentLvl;
    private static int winCounter = 0;
    bool isAboutMenuInstatillated = false, isUpgradesMenuInstatillated = false;
    private int[] winCounters = {27, 37, 55, 56, 51, 50, 46, 33, 56, 59};

    private const int MAX_LVL = 10;


    void Start()
    {
      PlayerPrefs.SetInt("CurrentLvl", 6); //for test
      currentLvl = PlayerPrefs.GetInt("CurrentLvl", 1);
      currentCounter = PlayerPrefs.GetInt("CurrentCounter", 0);
      winCounter = PlayerPrefs.GetInt("WinCounter", 0);
      canvasGameInfo.GetComponent<CanvasGameInfo>().StartGameInfo(currentLvl);
       
      //if there is a save - try to load it
      //if no - install current lvl prefabs:
      /*if (! GetComponent<SaveLoadManager>().tryLoadObjectsData(currentLvl))
      {
        StartNewLvl();
      }*/
      //winCounter = 0; //for test
      StartNewLvl();

      ShowMainMenu();
    }

    public void StartNewLvl() 
    {
      //Debug.Log("Start new lvl: " + currentLvl);
      PlayerPrefs.SetInt("CurrentLvl", currentLvl);
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
      canvasGameInfo.GetComponent<CanvasGameInfo>().setCoinsThisLevelToNull(); 

      currentCounter = 0;
      PlayerPrefs.SetInt("CurrentCounter", currentCounter);

      /*GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
      winCounter = allBlocks.Length;
      Debug.Log("winCounter: " + winCounter);*/
      winCounter = winCounters[currentLvl-1];
      PlayerPrefs.SetInt("WinCounter", winCounter);

      UndarkenScreenUnpauseTime();
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
        textGameScore.GetComponent<Text>().text = "SCORE: " + (score + 100); //+100 for last block
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
      PlayerPrefs.SetInt("Coins", 0);
      PlayerPrefs.SetInt("Score", 0);
      PlayerPrefs.SetInt("Balls", 3);
      PlayerPrefs.SetInt("CurrentLvl", 1);
      currentLvl = 1;

      PlayerPrefs.SetFloat("BarrierDuration", 5f);
      PlayerPrefs.SetFloat("PlasmaBallDuration", 5f);
      PlayerPrefs.SetInt("PriceLife", 25);
      PlayerPrefs.SetInt("PriceBarrier", 25);
      PlayerPrefs.SetInt("PricePlasma", 25);

      canvasGameInfo.GetComponent<CanvasGameInfo>().StartGameInfo(currentLvl);    
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
      SaveGame();
      mainMenu.SetActive(true);
    }

    public void HideMainMenu()
    {
      mainMenu.SetActive(false);
      UndarkenScreenUnpauseTime();
    }

    public void ShowPauseMenu()
    {
      DarkenScreenPauseTime();
      SaveGame();
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
        aboutMenu = Instantiate(aboutMenu, new Vector2(0, 0), Quaternion.identity);
        isAboutMenuInstatillated = true;
      }
    }

    public void OpenOrShowUpgradesMenu()
    {
      if (isUpgradesMenuInstatillated) {
        upgradesMenu.SetActive(true);
      } else {
        upgradesMenu = Instantiate(upgradesMenu, new Vector2(0, 0), Quaternion.identity);
        isUpgradesMenuInstatillated = true;
      }
      //todo - you can just get data with getter/args!
      canvasGameInfo.GetComponent<CanvasGameInfo>().saveData();
    }

    private void SaveGame()
    {
      PlayerPrefs.SetInt("CurrentLvl", currentLvl);
      PlayerPrefs.SetInt("CurrentCounter", currentCounter);
      PlayerPrefs.SetInt("WinCounter", winCounter);

      GetComponent<SaveLoadManager>().saveObjectsData();
      canvasGameInfo.GetComponent<CanvasGameInfo>().saveData();
    }

    public void QuitAndSave() 
    {
      SaveGame();
      Application.Quit();
    }

    void OnApplicationQuit()
    {
        QuitAndSave();
    }
}
