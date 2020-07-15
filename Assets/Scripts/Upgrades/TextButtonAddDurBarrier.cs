using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextButtonAddDurBarrier : MonoBehaviour
{
  void Start()
  {
      int priceBarrier = PlayerPrefs.GetInt("PriceBarrier", 25);
      UpdateButtonBarrier(priceBarrier);
  }

  public void UpdateButtonBarrier(int priceBarrierUpdated)
  {
      gameObject.GetComponent<Text>().text = "+0.5s for " + priceBarrierUpdated + " coins";
  }
}
