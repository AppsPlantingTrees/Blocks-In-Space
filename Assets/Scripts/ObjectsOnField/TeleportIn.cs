using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Simple TeleportIn class, collision with ball
moves ball into one of randomly choosed TeleportOut.
*/

public class TeleportIn : MonoBehaviour
{
  private int stuckCounter = 0;

  void OnTriggerEnter2D(Collider2D collisionInfo) {
      stuckCounter++;
      //if ball stuck, change it's position a bit:
      if (stuckCounter < 5) {
        GameObject[] allTelOut = GameObject.FindGameObjectsWithTag("TeleportOut");
        int i = Random.Range(0, allTelOut.Length);
        collisionInfo.transform.position = new Vector2(allTelOut[i].transform.position.x, 
                allTelOut[i].transform.position.y);
      } else {
        stuckCounter = 0;
      }
  }

  public ObjectForSave GetTelInForSave() 
  {
    return new ObjectForSave() {
       position_x = transform.position.x,
       position_y = transform.position.y
    };
  }
}
