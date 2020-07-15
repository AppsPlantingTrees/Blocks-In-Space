using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Bonus
{
  void OnCollisionEnter2D(Collision2D collisionInfo)
  {
     if (collisionInfo.gameObject.tag == "Racket")
     {
        GameObject.FindWithTag("CanvasGameInfo").GetComponent<CanvasGameInfo>().LivesPlus();
        Destroy(gameObject);
     }
   }
}
