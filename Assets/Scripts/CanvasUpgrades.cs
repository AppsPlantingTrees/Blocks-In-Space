using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class CanvasUpgrades : MonoBehaviour
{
  private int coins, balls, priceLife, priceBarrier, pricePlasma;
  private int addedCoins;
  private int adCounter = 0;
  private const int ADS_PER_DAY = 1; //ads at the end of lvl not counts here
  float barrierDuration, plasmaBallDuration;
  public GameObject amountOfCoins, textPlasma, textBarrier, textBall, buttonAd,
  textButtonAd, textButtonAddDurPlasma, textButtonAddDurBarrier, textButtonAddBall;

  void Start()
  {
    addedCoins = 0;
    loadData();
    if (adCounter >= ADS_PER_DAY)
    {
      Debug.Log("stop ads, adCounter: " + adCounter);
      textButtonAd.GetComponent<Text>().text = "No more ads here for today";
      buttonAd.GetComponent<Button>().interactable = false;
    }
  }

  void loadData()
  {
    //counter to disable video ads here after 15/day:
    int currentDay = System.DateTime.Now.Day;
    Debug.Log("currentDay: " + currentDay);
    if (currentDay != PlayerPrefs.GetInt("SavedDay", 0)) {
      adCounter = 0;
      PlayerPrefs.SetInt("SavedDay", currentDay);
    } else {
      adCounter = PlayerPrefs.GetInt("AdCounter", 0);
    }
  
    coins = PlayerPrefs.GetInt("Coins", 0);
    balls = PlayerPrefs.GetInt("Balls", 3);
    barrierDuration = PlayerPrefs.GetFloat("BarrierDuration", 5f);
    plasmaBallDuration = PlayerPrefs.GetFloat("PlasmaBallDuration", 5f);
    priceLife = PlayerPrefs.GetInt("PriceLife", 25);
    priceBarrier = PlayerPrefs.GetInt("PriceBarrier", 25);
    pricePlasma = PlayerPrefs.GetInt("PricePlasma", 25);
  }

  public void watchVideoAd()
  {
    //use Video ads for newer Android versions and Interstitial for old:
    int typeOfAd = Appodeal.REWARDED_VIDEO;
    int androidlvl = getSDKInt();
    if (androidlvl < 21) //21 for LOLLIPOP
    {
      typeOfAd = Appodeal.INTERSTITIAL;
    }

    if (Appodeal.canShow(typeOfAd))
    {
      adCounter++;
      Appodeal.show(typeOfAd);
    }

    coins += 10;
    addedCoins += 10;
    amountOfCoins.GetComponent<AmountOfCoins>().UpdateCoins(coins);
  }

  public void buyPlasmaTime()
  {
    if (coins - pricePlasma > -1)
    {
      coins -= pricePlasma;
      addedCoins -= pricePlasma;
      amountOfCoins.GetComponent<AmountOfCoins>().UpdateCoins(coins);
      plasmaBallDuration = plasmaBallDuration + 0.5f;
      textPlasma.GetComponent<TextPlasma>().UpdateTextPlasma(plasmaBallDuration);

      pricePlasma = pricePlasma + 25;
      textButtonAddDurPlasma.GetComponent<TextButtonAddDurPlasma>().UpdateButtonPlasma(pricePlasma);
    }
  }

  public void buyBarrierTime()
  {
    if (coins - priceBarrier > -1)
    {
      coins -= priceBarrier;
      addedCoins -= priceBarrier;
      amountOfCoins.GetComponent<AmountOfCoins>().UpdateCoins(coins);
      barrierDuration = barrierDuration + 0.5f;
      textBarrier.GetComponent<TextBarrier>().UpdateTextBarrier(barrierDuration);

      priceBarrier = priceBarrier + 25;
      textButtonAddDurBarrier.GetComponent<TextButtonAddDurBarrier>().UpdateButtonBarrier(priceBarrier);
    }
  }

  public void buyLife()
  {
    if (coins - priceLife > -1)
    {
      coins -= priceLife;
      addedCoins -= priceLife;
      amountOfCoins.GetComponent<AmountOfCoins>().UpdateCoins(coins);
      balls = balls + 1;
      textBall.GetComponent<TextBall>().UpdateBalls(balls);

      if (priceLife < 99)
      {
        priceLife = priceLife + 5;
        textButtonAddBall.GetComponent<TextButtonAddBall>().UpdateButtonAddBall(priceLife);
      }
    }
  }

  public void HideMenuUpgrades()
  {
    saveData();    
    GameObject canvasGameInfo = GameObject.FindWithTag("CanvasGameInfo");
    canvasGameInfo.GetComponent<CanvasGameInfo>().UpdateLives(balls);
    canvasGameInfo.GetComponent<CanvasGameInfo>().UpdateGold(addedCoins);
    addedCoins = 0;
    gameObject.SetActive(false);
  }

  void saveData()
  {
    PlayerPrefs.SetInt("AdCounter", adCounter);
    PlayerPrefs.SetInt("Coins", coins);
    PlayerPrefs.SetInt("Balls", balls);
    PlayerPrefs.SetFloat("BarrierDuration", barrierDuration);
    PlayerPrefs.SetFloat("PlasmaBallDuration", plasmaBallDuration);
    PlayerPrefs.SetInt("PriceLife", priceLife);
    PlayerPrefs.SetInt("PriceBarrier", priceBarrier);
    PlayerPrefs.SetInt("PricePlasma", pricePlasma);
  }

  static int getSDKInt() 
  {
      using (var version = new AndroidJavaClass("android.os.Build$VERSION")) {
        return version.GetStatic<int>("SDK_INT");
      }
  }
}
