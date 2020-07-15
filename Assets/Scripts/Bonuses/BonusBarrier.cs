using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBarrier : Bonus
{
  public GameObject barrier;

   public override void GetBonus()
   {
       Instantiate(barrier, new Vector2(-1, -105), Quaternion.identity);
   }
}
