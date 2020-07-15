using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpeedUp : Bonus
{
  const int SPEED_UP = 1;

   public override void GetBonus()
   {
       GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
       BallPlasma pb = (BallPlasma)FindObjectOfType(typeof(BallPlasma));

       if (!pb) {
         foreach(GameObject ball in allBalls)
         {
           ball.GetComponent<Ball>().ChangeSpeed(SPEED_UP);
         }
       } else {
         foreach(GameObject ball in allBalls)
         {
             ball.GetComponent<BallPlasma>().ChangeSpeed(SPEED_UP);
          }
       }
   }
}
