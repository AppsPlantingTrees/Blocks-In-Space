using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextButtonAddDurPlasma : MonoBehaviour
{
  void Start()
  {
      int pricePlasma = PlayerPrefs.GetInt("PricePlasma", 25);
      UpdateButtonPlasma(pricePlasma);
  }

  public void UpdateButtonPlasma(int pricePlasmaUpdated)
  {
      gameObject.GetComponent<Text>().text = "+0.5s for " + pricePlasmaUpdated + " coins";
  }
}
