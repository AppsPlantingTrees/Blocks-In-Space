using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountOfCoins : MonoBehaviour
{
    void Start()
    {
        int coins = PlayerPrefs.GetInt("Coins");
        UpdateCoins(coins);
    }

    public void UpdateCoins(int coinsUpdate)
    {
        gameObject.GetComponent<Text>().text = coinsUpdate + "";
    }
}
