using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPlasma : Bonus
{
  public GameObject plasmaBall;

   public override void GetBonus()
   {
       //makes all balls plasma
       //start of plasma ball make all bricks cuttable
       GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
       foreach(GameObject ball in allBalls)
       {
         GameObject obj = Instantiate(plasmaBall, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
         obj.GetComponent<Rigidbody2D>().velocity = ball.GetComponent<Rigidbody2D>().velocity;
         Destroy(ball.gameObject);
       }
   }
}
