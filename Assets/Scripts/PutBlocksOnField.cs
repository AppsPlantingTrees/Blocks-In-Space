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
    private int[] map={     -1, -1, -1, -1, -1, -1, -1,
                            -1, 11, -1, -1, -1, -1, -1,
                            -1, -1, -1, 7, -1, -1, -1,
                            -1, -1, 7, 20, 7, -1, -1,
                            -1, -1, -1, 7, -1, 9, -1,
                            -1, 7, -1, -1, -1, -1, -1,
                            7, 20, 7, -1, -1, 2, -1,
                            -1, 7, -1, -1, 2, 20, 2,
                            -1, -1, -1, -1, -1, 2, -1,
                            -1, -1, -1, -1, -1, -1, -1,
                             7, 7, 9, 7, 7, 20, 7,
                            7, 20, 7, 7, 11, 7, 7 };

    public Block[] blocksArray;
    private int xLen = 18, yLen = 8;
    private int xSpaceLen = 1, ySpaceLen = 1; //space between blocks, 2 2 default
    private int xStart = -60, yStart = -40;
    private int xPos, yPos;

    void Start()
    {
        PutBlocks();
        //PutBlocksInCircle(16, 1, 60f);
        /*PutBlocksInCircle(13, 5, 50f);
        PutBlocksInCircle(10, 0, 39f);
        PutBlocksInCircle(7, 0, 27f);
        PutBlocksInCircle(4, 4, 14f);*/
        //PutBlocksInRectangle(3, 12, 1);
        //PutBlocksInWave(1, 7, 5);
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

    public void PutBlocksInWave(int h, int w, int typeOfBlocks)
    {
        xPos = xStart;
        yPos = yStart;
        int lineCounter = 0;
        for (int i = h*w; i > 0; i--)
        {
            Instantiate(blocksArray[typeOfBlocks], new Vector2(xPos, yPos), Quaternion.identity);
            xPos += (xLen + xSpaceLen);
            if (i%2 == 0) {
                yPos += 5;
            } else {
                yPos -= 5;
            }
            lineCounter++;
            if (lineCounter >= 7) 
            {
                xPos = xStart;
                yPos += (yLen + ySpaceLen);
                lineCounter = 0;
            }
        }
    }

    public void PutBlocksInRectangle(int h, int w, int typeOfBlocks)
    {
        int xPos = xStart;
        int yPos = yStart;
        int lineCounter = 0;
        for (int i = h*w; i > 0; i--)
        {
            Instantiate(blocksArray[typeOfBlocks], new Vector2(xPos, yPos), Quaternion.identity);
            xPos += (xLen + xSpaceLen);
            lineCounter++;
            if (lineCounter >= w) 
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
