using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBackground : MonoBehaviour
{
    public Sprite[] backgroundsArray;
    public Image background;

    public void SetUpBackground(int lvl) 
    {
      try {
        background.GetComponent<Image>().sprite = backgroundsArray[lvl-1];
      }
      catch {
        Debug.Log("Can't open background " + (lvl-1));
      }
    }
   
}
