using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusShortRacket : Bonus
{
   public override void GetBonus()
   {
      Racket racket = GameObject.FindWithTag("Racket").GetComponent<Racket>();
      //if it's long racket, make it normal:
      if (racket.lenRacket == 3) {
         racket.setNormalRacket(racket.transform.position.x, racket.transform.position.y, true);
      //if normal, make it short:
      } else {
         racket.setShortRacket(racket.transform.position.x, racket.transform.position.y, true);
      }
   }
}
