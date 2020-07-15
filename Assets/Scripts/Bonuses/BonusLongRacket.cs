using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLongRacket : Bonus
{
  public GameObject longRacket;

   public override void GetBonus()
   {
       GameObject racket = GameObject.FindWithTag("Racket");
       Instantiate(longRacket, new Vector2(racket.transform.position.x, racket.transform.position.y), Quaternion.identity);
       Destroy(racket.gameObject);
   }
}
