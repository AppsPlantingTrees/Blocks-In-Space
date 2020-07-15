using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUpgrades : MonoBehaviour
{
  int coins, balls, priceLife, priceBarrier, pricePlasma;
  float barrierDuration, plasmaBallDuration;
  public GameObject amountOfCoins, textPlasma, textBarrier, textBall,
  textButtonAddDurPlasma, textButtonAddDurBarrier, textButtonAddBall;

  void Start()
  {
    loadData();
  }

  void loadData()
  {
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
    Debug.Log("Watching video ad...");
    coins = coins + 10;
    amountOfCoins.GetComponent<AmountOfCoins>().UpdateCoins(coins);
  }

  public void buyPlasmaTime()
  {
    if (coins - pricePlasma > -1)
    {
      coins = coins - pricePlasma;
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
      coins = coins - priceBarrier;
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
      coins = coins - priceLife;
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
    canvasGameInfo.GetComponent<CanvasGameInfo>().UpdateGold(coins);
    //todo - may "this" would work??
    GameObject upgradesMenu = GameObject.FindWithTag("MenuUpgrades");
    upgradesMenu.SetActive(false);
  }

  void saveData()
  {
    PlayerPrefs.SetInt("Coins", coins);
    PlayerPrefs.SetInt("Balls", balls);
    PlayerPrefs.SetFloat("BarrierDuration", barrierDuration);
    PlayerPrefs.SetFloat("PlasmaBallDuration", plasmaBallDuration);
    PlayerPrefs.SetInt("PriceLife", priceLife);
    PlayerPrefs.SetInt("PriceBarrier", priceBarrier);
    PlayerPrefs.SetInt("PricePlasma", pricePlasma);
  }
}
