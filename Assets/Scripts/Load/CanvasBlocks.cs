using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBlocks : MonoBehaviour
{
    public Canvas[] levelsArray;

    public Canvas SetUpBlocks(int lvl) 
    {
      try {
        return Instantiate(levelsArray[lvl-1], new Vector2(0, 0), Quaternion.identity); 
      }
      catch {
        Debug.Log("Can't set up blocks for lvl " + (lvl-1));
        return null;
      }
    }
}
