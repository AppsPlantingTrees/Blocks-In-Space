using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

/*
Main object, manage game state, menus and screens,
count levels and broken blocks. Loads ads.
*/

public class GameManager : MonoBehaviour
{
    public GameObject winScreen, loseScreen, winGameScreen, textGameScore;
    public GameObject mainMenu, pauseMenu, canvasDarkenScreen, canvasGameInfo, canvasBlocks;
    public GameObject aboutMenu, upgradesMenu; //not initialized
    public GameObject soundManager;
    public Ball ball;
    public BallPlasma ballPlasma;
    public Racket racket;
    public Button soundButton;
    public Sprite soundButtonOn, soundButtonOff;
    Canvas blocks;

    float barrierDuration, plasmaBallDuration;
    private static int currentCounter = 0, currentLvl;
    private static int winCounter = 0;
    private int hasSound;
    bool isAboutMenuInstatillated = false, isUpgradesMenuInstatillated = false;
    private const string APPODEAL_KEY = "b5460f397e403c19683b360077da0fe5c73082a06764ee71";
    private const bool IS_APPODEAL_TEST = false; //for test
    private static int typeOfAd;

    private int[] winCounters = { 27, 37, 55, 56, 51, 
                                  50, 48, 33, 56, 59, 
                                  35, 46, 42, 40, 43,
                                  70, 35, 50, 48, 34,
                                  59, 46, 39, 43, 28,
                                  44, 32, 28, 31, 44, 
                                  44, 38, 38, 26, 39, };

    private const int MAX_LVL = 35;


    void Start()
    {
      setUpAppodealAds();

      //PlayerPrefs.SetInt("CurrentLvl", 35); //for test
      currentLvl = PlayerPrefs.GetInt("CurrentLvl", 1);
      currentCounter = PlayerPrefs.GetInt("CurrentCounter", 0);
      winCounter = PlayerPrefs.GetInt("WinCounter", 0);
      canvasGameInfo.GetComponent<CanvasGameInfo>().StartGameInfo(currentLvl);
       
      //if there is a save - try to load it
      //if no - install current lvl prefabs:
      if (! GetComponent<SaveLoadManager>().tryLoadObjectsData(currentLvl))
      {
        StartNewLvl();
      }
      //StartNewLvl(); //for test
      //winCounter = 0; //for test

      hasSound = PlayerPrefs.GetInt("hasSound", 0);
      if (hasSound == 0) {
        soundButton.GetComponent<Image>().sprite = soundButtonOff;
        soundManager.GetComponent<SoundManager>().stopPlaying();
      } else {
        soundButton.GetComponent<Image>().sprite = soundButtonOn;
      }

      ShowMainMenu();
    }

    public void StartNewLvl() 
    {
      Debug.Log("Start new lvl: " + currentLvl);
      PlayerPrefs.SetInt("CurrentLvl", currentLvl);
      CleanField();
      winScreen.SetActive(false);
      loseScreen.SetActive(false);
  
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

      GameObject[] allExplosions = GameObject.FindGameObjectsWithTag("Explosion");
      foreach(GameObject explosion in allExplosions)
      {
        Destroy(explosion);
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
      if (currentLvl < MAX_LVL)
      {
        soundManager.GetComponent<SoundManager>().playWinLvl();
        DarkenScreenPauseTime();
        SaveGame();
        winScreen.SetActive(true);
        winScreen.GetComponent<CanvasWinLvl>().SetUpCanvas(currentLvl, typeOfAd);        
      } else {
        //TODO install this screen, not inicialize
        //it was last lvl, you win the game:
        soundManager.GetComponent<SoundManager>().playWinGame();
        canvasDarkenScreen.SetActive(true);
        SaveGame();
        CleanField();
        winGameScreen.SetActive(true);
        int score = canvasGameInfo.GetComponent<CanvasGameInfo>().getScore() + 100; //+100 for last block
        winGameScreen.GetComponent<CanvasWinGame>().SetUpCanvas(score);    
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
      //count blocks one more time in case of bugs:
      GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
      if (allBlocks.Length <= 0)
      {
        canvasDarkenScreen.SetActive(false);
        Win();
        return;
      } 
      canvasDarkenScreen.SetActive(false);
      Time.timeScale = 1.0f;
    }

    public void NewGame()
    {
      soundManager.GetComponent<SoundManager>().stopPlaying();

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
      soundManager.GetComponent<SoundManager>().stopPlaying();
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
      //todo - you can just get data with getter/args!
      canvasGameInfo.GetComponent<CanvasGameInfo>().saveData();

      if (isUpgradesMenuInstatillated) {
        upgradesMenu.SetActive(true);
      } else {
        upgradesMenu = Instantiate(upgradesMenu, new Vector2(0, 0), Quaternion.identity);
        isUpgradesMenuInstatillated = true;
      }
      upgradesMenu.GetComponent<CanvasUpgrades>().startUpgrades(typeOfAd);
    }

    private void SaveGame()
    {
      PlayerPrefs.SetInt("CurrentLvl", currentLvl);
      PlayerPrefs.SetInt("CurrentCounter", currentCounter);
      PlayerPrefs.SetInt("WinCounter", winCounter);
      PlayerPrefs.SetInt("hasSound", hasSound);

      GetComponent<SaveLoadManager>().saveObjectsData(currentLvl);
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

    private void setUpAppodealAds() 
    {
      //use Video ads for newer Android versions and Interstitial for old:
      typeOfAd = Appodeal.REWARDED_VIDEO;
      int androidlvl = getSDKInt();
      if (androidlvl < 21) //21 for LOLLIPOP
      {
        typeOfAd = Appodeal.INTERSTITIAL;
      }

      //disable unused/unnessesary stuff:
      Appodeal.disableNetwork(AppodealNetworks.STARTAPP);
      Appodeal.disableNetwork(AppodealNetworks.MINTEGRAL);
      Appodeal.disableNetwork(AppodealNetworks.MY_TARGET);
      Appodeal.disableNetwork(AppodealNetworks.YANDEX);
      Appodeal.disableNetwork(AppodealNetworks.SMAATO);
      Appodeal.disableNetwork(AppodealNetworks.FACEBOOK);
      Appodeal.disableLocationPermissionCheck();
      Appodeal.disableWriteExternalStoragePermissionCheck();

      Appodeal.setTesting(IS_APPODEAL_TEST);

      //check consent, defaut is false:
      bool consent = false;
      int cons = PlayerPrefs.GetInt("result_gdpr_sdk", 0);
      if (cons == 1) consent = true;
      Appodeal.initialize(APPODEAL_KEY, typeOfAd, consent);
    }

    static int getSDKInt() 
    {
       using (var version = new AndroidJavaClass("android.os.Build$VERSION")) {
         return version.GetStatic<int>("SDK_INT");
       }
    }

    public void onClickSoundButton()
    {
      if (hasSound == 0) {
        hasSound = 1;
        soundButton.GetComponent<Image>().sprite = soundButtonOn;
      } else {
        hasSound = 0;
        soundButton.GetComponent<Image>().sprite = soundButtonOff;
        soundManager.GetComponent<SoundManager>().stopPlaying();
      }
      PlayerPrefs.SetInt("hasSound", hasSound);
      soundManager.GetComponent<SoundManager>().updateSoundPrefs();
    }

    public void setCurrentLvl(int lvl) 
    {
      currentLvl = lvl;
    }
}
