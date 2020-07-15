using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPlasma : MonoBehaviour
{
  void Start()
  {
      float plasmaDuration = PlayerPrefs.GetFloat("PlasmaBallDuration");
      UpdateTextPlasma(plasmaDuration);
  }

  public void UpdateTextPlasma(float plasmaDurationUpdate)
  {
      gameObject.GetComponent<Text>().text = "Plasma: " + plasmaDurationUpdate + "s";
  }
}
