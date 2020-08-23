using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountOfCoins : MonoBehaviour
{
    public void UpdateCoins(int coinsUpdate)
    {
        gameObject.GetComponent<Text>().text = coinsUpdate + "";
    }
}
