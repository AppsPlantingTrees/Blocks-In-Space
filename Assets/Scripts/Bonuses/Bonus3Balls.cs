using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus3Balls : Bonus
{
  public GameObject ballCopy;
  public GameObject plasmaBallCopy;
  float speed = 100.0f;

   public override void GetBonus()
   {
       BallPlasma pb = (BallPlasma)FindObjectOfType(typeof(BallPlasma));

       GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
       if (!pb) {
         foreach(GameObject ball in allBalls)
         {
            GameObject obj = Instantiate(ballCopy, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 10);
            GameObject obj2 = Instantiate(ballCopy, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            obj2.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, speed);
         }
       } else {
         foreach(GameObject ball in allBalls)
         {
            GameObject obj = Instantiate(plasmaBallCopy, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 10);
            GameObject obj2 = Instantiate(plasmaBallCopy, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            obj2.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, speed);
          }
       }
   }
}
