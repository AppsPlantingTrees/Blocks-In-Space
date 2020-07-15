using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBarrier : MonoBehaviour
{
  void Start()
  {
      float barrierDuration = PlayerPrefs.GetFloat("BarrierDuration");
      UpdateTextBarrier(barrierDuration);
  }

  public void UpdateTextBarrier(float barrierDurationUpdate)
  {
      gameObject.GetComponent<Text>().text = "Barrier: " + barrierDurationUpdate + "s";
  }
}
