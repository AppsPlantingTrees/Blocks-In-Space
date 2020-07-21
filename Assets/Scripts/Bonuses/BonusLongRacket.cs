using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLongRacket : Bonus
{
   public override void GetBonus()
   {
      Racket racket = GameObject.FindWithTag("Racket").GetComponent<Racket>();
      //if it's short racket, make it normal:
      if (racket.lenRacket == 1) {
         racket.setNormalRacket(racket.transform.position.x, racket.transform.position.y, true);
      //if it's normal make it long:
      } else {
         racket.setLongRacket(racket.transform.position.x, racket.transform.position.y, true);
      }  
   }
}
