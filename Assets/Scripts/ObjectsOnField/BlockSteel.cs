using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSteel : Block
{
    //rewrite OnCollision so this block wouldn't destroy from a regular ball
    void OnCollisionEnter2D(Collision2D collisionInfo) 
    {
    }

    public override void OnHit() 
    {
        GameObject.FindWithTag("CanvasGameInfo").GetComponent<CanvasGameInfo>().UpdateScore(SCORE_FOR_BLOCK);
        Destroy(gameObject);
    }
}
