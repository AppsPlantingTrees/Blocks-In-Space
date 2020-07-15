using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOut : MonoBehaviour
{
  public ObjectForSave GetTelOutForSave() 
  {
    return new ObjectForSave() {
       position_x = transform.position.x,
       position_y = transform.position.y
    };
  }
}
