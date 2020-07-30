using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSmallerBall : Bonus
{
   public override void GetBonus()
   {
       //makes all balls smaller
       GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
       foreach(GameObject ball in allBalls)
       {
         Vector3 newScale = new Vector3(ball.gameObject.transform.localScale.x/1.8f, ball.gameObject.transform.localScale.y/1.8f, 1);
         ball.gameObject.transform.localScale = newScale;
       }
   }
}