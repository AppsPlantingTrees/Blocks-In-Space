using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusShortRacket : Bonus
{
  public GameObject shortRacket;

   public override void GetBonus()
   {
       GameObject racket = GameObject.FindWithTag("Racket");
       Instantiate(shortRacket, new Vector2(racket.transform.position.x, racket.transform.position.y), Quaternion.identity);
       Destroy(racket.gameObject);
   }
}
