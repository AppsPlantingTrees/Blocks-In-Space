using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBall : MonoBehaviour
{
  void Start()
  {
      int balls = PlayerPrefs.GetInt("Balls");
      UpdateBalls(balls);
  }

  public void UpdateBalls(int ballsUpdate)
  {
      gameObject.GetComponent<Text>().text = "Balls: " + ballsUpdate;
  }
}
