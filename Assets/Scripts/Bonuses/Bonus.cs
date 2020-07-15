using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Main Bonus class, OnCollision with Racket applies
it's bonus.
*/

public class Bonus : MonoBehaviour
{
   public string typeOfBonus = "Bonus";
   public const int SCORE_FOR_BONUS = 300;

   void OnCollisionEnter2D(Collision2D collisionInfo)
   {
      if (collisionInfo.gameObject.tag == "Racket")
      {
         gameObject.GetComponent<Bonus>().GetBonus();
         GameObject.FindWithTag("CanvasGameInfo").GetComponent<CanvasGameInfo>().UpdateScore(SCORE_FOR_BONUS);
         Destroy(gameObject);
      }
   }

   public virtual void GetBonus(){}

   public BonusForSave GetBonusForSave() 
   {
      return new BonusForSave() {
         position_x = transform.position.x,
         position_y = transform.position.y,
         velocity_x = this.GetComponent<Rigidbody2D>().velocity.x,
         velocity_y = this.GetComponent<Rigidbody2D>().velocity.y,
         type = typeOfBonus
      };
   }
}