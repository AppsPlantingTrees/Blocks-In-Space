using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBlocksFromSave : MonoBehaviour
{
    public Block[] blocksArray;

    public void LoadBlocksFromSave(List<BlockForSave> allBlocks, List<BlockForSave> allSteelBlocks)
    {
        foreach(BlockForSave blockToLoad in allBlocks)
        {
            int index = 0;
            switch (blockToLoad.type)
            {
            case "Blue":
                index = 0;
                break;
            case "Green":
                index = 1;
                break;
            case "Orange":
                index = 2;
                break;
            case "Red":
                index = 3;
                break;
            case "Violet":
                index = 4;
                break;
            case "Yellow":
                index = 5;
                break;
            case "Steel":
                index = 6;
                break;
            case "Stone":
                index = 7;
                break;
            case "StoneHalfBrocken":
                index = 8;
                break;
            case "BlockBonus3Balls":
                index = 9;
                break;
            case "BlockBonusBarrier":
                index = 10;
                break;
            case "BlockBonusDeath":
                index = 11;
                break;
            case "BlockBonusLife":
                index = 12;
                break;
            case "BlockBonusLongRacket":
                index = 13;
                break;
            case "BlockBonusPlasma":
                index = 14;
                break;
            case "BlockBonusShortRacket":
                index = 15;
                break;
            case "BlockBonusSlowDown":
                index = 16;
                break;
            case "BlockBonusSpeedUp":
                index = 17;
                break;
            case "BlockBonusEnlargeBall":
                index = 18;
                break;
            case "BlockBonusSmallerBall":
                index = 19;
                break;
            }
            Block b = Instantiate(blocksArray[index], new Vector2(blockToLoad.position_x, 
                    blockToLoad.position_y), Quaternion.identity);
            b.transform.rotation = Quaternion.Euler(Vector3.forward * blockToLoad.rotation);

        }

        foreach(BlockForSave blockToLoad in allSteelBlocks)
        {
            Block b = Instantiate(blocksArray[6], new Vector2(blockToLoad.position_x, 
                    blockToLoad.position_y), Quaternion.identity);
             b.transform.rotation = Quaternion.Euler(Vector3.forward * blockToLoad.rotation);
        }
    }
}
