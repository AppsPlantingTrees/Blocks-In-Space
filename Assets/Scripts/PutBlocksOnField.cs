using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Helper functions to paint new map. 
Place blocks on screen in circle or/and in rectange (based on map).
*/

public class PutBlocksOnField : MonoBehaviour
{
    /*private int[] map = { -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                            -1, -1, -1, -1, -1, -1, -1 };*/
    private int[] map={ -1, -1, 0, 0, 0, 0, 0,
                        -1, -1, 0, 0, 0, 0, 0,
                        -1, -1, 0, 0, 0, 0, 0,
                        -1, -1, 0, 0, 0, 0, 0,
                        -1, -1, 0, 0, 0, 0, 0,
                        -1, -1, 0, 0, 0, 0, 0,
                        -1, -1, 0, 0, 0, 0, 0,
                        -1, -1, 0, 0, 0, 0, 0,
                        -1, -1, -1, -1, -1, -1, -1,
                        -1, -1, -1, -1, -1, -1, -1,
                        -1, -1, -1, -1, -1, -1, -1,
                        -1, -1, -1, -1, -1, -1, -1  };

    public Block[] blocksArray;
    private int xLen = 18, yLen = 8;
    private int xSpaceLen = 2, ySpaceLen = 2; //space between blocks
    private int xStart = -60, yStart = -40;
    private int xPos, yPos;

    void Start()
    {
        //PutBlocks();
        PutBlocksInCircle(6, 2, 22f);
    }

    public void PutBlocks()
    {
        xPos = xStart;
        yPos = yStart;
        int lineCounter = 0;
        for (int i = map.Length-1; i >= 0; i--)
        {
            if (map[i] >= 0) 
            {
                Instantiate(blocksArray[map[i]], new Vector2(xPos, yPos), Quaternion.identity);
            }
            xPos += (xLen + xSpaceLen);
            lineCounter++;
            if (lineCounter >= 7) 
            {
                xPos = xStart;
                yPos += (yLen + ySpaceLen);
                lineCounter = 0;
            }
        }
    }

    public void PutBlocksInCircle(int amountOfBlocks, int typeOfBlocks, float radius)
    {
        float rotation = 90;
        for (int i = 0; i < amountOfBlocks; i++)
        {
            float angle = i * Mathf.PI * 2f / amountOfBlocks;
            Vector2 newPos = new Vector2(Mathf.Cos(angle)*radius, Mathf.Sin(angle)*radius);
            Block b = Instantiate(blocksArray[typeOfBlocks], newPos, Quaternion.identity);
            b.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
            rotation += 90 * 4f / amountOfBlocks ;
        }
    }

}
