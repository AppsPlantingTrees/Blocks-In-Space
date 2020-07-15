using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackground : MonoBehaviour
{
    public Sprite[] backgroundsArray;
    public GameObject background;

    public void SetUpBackground(int lvl) 
    {
      try {
        background.GetComponent<SpriteRenderer>().sprite = backgroundsArray[lvl-1];
      }
      catch {
        Debug.Log("Can't open background " + (lvl-1));
      }
    }
   
}
