using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus3Balls : Bonus
{
  public GameObject ballCopy;
  public GameObject plasmaBallCopy;

   public override void GetBonus()
   {
       BallPlasma pb = (BallPlasma)FindObjectOfType(typeof(BallPlasma));

       GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
       if (!pb) {
         foreach(GameObject ball in allBalls)
         {
            Vector3 scale = new Vector3(ball.gameObject.transform.localScale.x, ball.gameObject.transform.localScale.y, 1);
            GameObject obj = Instantiate(ballCopy, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(100, 50);
            obj.gameObject.transform.localScale = scale;
            GameObject obj2 = Instantiate(ballCopy, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            obj2.GetComponent<Rigidbody2D>().velocity = new Vector2(50, 100);
            obj2.gameObject.transform.localScale = scale;
         }
       } else {
         foreach(GameObject ball in allBalls)
         {
           Vector3 scale = new Vector3(ball.gameObject.transform.localScale.x, ball.gameObject.transform.localScale.y, 1);
            GameObject obj = Instantiate(plasmaBallCopy, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(100, 50);
            obj.gameObject.transform.localScale = scale;
            GameObject obj2 = Instantiate(plasmaBallCopy, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            obj2.GetComponent<Rigidbody2D>().velocity = new Vector2(50, 100);
            obj2.gameObject.transform.localScale = scale;
          }
       }
   }
}
