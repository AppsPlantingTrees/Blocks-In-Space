using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelsFromSave : MonoBehaviour
{
    public TeleportIn telIn;
    public TeleportOut telOut;

    public void LoadTelsFromSave(List<ObjectForSave> telsIn, List<ObjectForSave> telsOut)
    {
        foreach(ObjectForSave telInToLoad in telsIn)
        {
            Instantiate(telIn, new Vector2(telInToLoad.position_x, telInToLoad.position_y), Quaternion.identity);
        }

        foreach(ObjectForSave telOutToLoad in telsOut)
        {
            Instantiate(telOut, new Vector2(telOutToLoad.position_x, telOutToLoad.position_y), Quaternion.identity);
        }
    }
}
