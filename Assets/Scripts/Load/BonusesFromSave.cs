using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusesFromSave : MonoBehaviour
{
public Bonus[] bonusesArray;

    public void LoadBonusesFromSave(List<BonusForSave> allBonuses)
    {
        foreach(BonusForSave bonusToLoad in allBonuses)
        {
            int index = 0;
            switch (bonusToLoad.type)
            {
            case "Bonus3Balls":
                index = 0;
                break;
            case "BonusBarrier":
                index = 1;
                break;
            case "BonusLongRacket":
                index = 2;
                break;
            case "BonusMagnet":
                index = 3;
                break;
            case "BonusPlasma":
                index = 4;
                break;
            case "BonusShortRacket":
                index = 5;
                break;
            case "BonusSlowDown":
                index = 6;
                break;
            case "BonusSpeedUp":
                index = 7;
                break;
            case "Death":
                index = 8;
                break;
            case "Life":
                index = 9;
                break;
            case "BonusEnlargeBall":
                index = 10;
                break;
            case "BonusSmallerBall":
                index = 11;
                break;
            }
            Bonus b = Instantiate(bonusesArray[index], new Vector2(bonusToLoad.position_x, 
                    bonusToLoad.position_y), Quaternion.identity);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(bonusToLoad.velocity_x, 
                    bonusToLoad.velocity_y);

        }
    }
}
