using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Bonus
{
  void OnCollisionEnter2D(Collision2D collisionInfo)
  {
    if (collisionInfo.gameObject.tag == "Racket")
    {
      Destroy(collisionInfo.gameObject);
      soundManager.GetComponent<SoundManager>().playCatchDeath();
      Destroy(gameObject);
    }
   }
}
