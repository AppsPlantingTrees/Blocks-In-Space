using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSlowDown : Bonus
{
  const int SLOW_DOWN = 0;

   public override void GetBonus()
   {
     GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
     BallPlasma pb = (BallPlasma)FindObjectOfType(typeof(BallPlasma));

     if (!pb) {
       foreach(GameObject ball in allBalls)
       {
         ball.GetComponent<Ball>().ChangeSpeed(SLOW_DOWN);
       }
     } else {
       foreach(GameObject ball in allBalls)
       {
           ball.GetComponent<BallPlasma>().ChangeSpeed(SLOW_DOWN);
        }
     }
   }
}
