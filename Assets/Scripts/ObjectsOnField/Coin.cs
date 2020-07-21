using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Coin class, collision with Racket increases amount of coins.
*/

public class Coin : MonoBehaviour
{
   void OnCollisionEnter2D(Collision2D collisionInfo)
   {
      if (collisionInfo.gameObject.tag == "Racket")
      {
         GameObject.FindWithTag("CanvasGameInfo").GetComponent<CanvasGameInfo>().UpdateGoldCatchedCoin();
         Destroy(gameObject);           
      }
   }

   public MovingObjectForSave GetCoinForSave() 
   {
      return new MovingObjectForSave() {
         position_x = transform.position.x,
         position_y = transform.position.y,
         velocity_x = this.GetComponent<Rigidbody2D>().velocity.x,
         velocity_y = this.GetComponent<Rigidbody2D>().velocity.y
      };
   }
}
