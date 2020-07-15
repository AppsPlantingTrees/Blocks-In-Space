using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextButtonAddBall : MonoBehaviour
{
    void Start()
    {
        int priceLife = PlayerPrefs.GetInt("PriceLife", 25);
        UpdateButtonAddBall(priceLife);
    }

    public void UpdateButtonAddBall(int priceLifeUpdated)
    {
        gameObject.GetComponent<Text>().text = "+1 ball for " + priceLifeUpdated + " coins";
    }
}
