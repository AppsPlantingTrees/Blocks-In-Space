using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStone : Block {

    public GameObject HalfBrokenBlock;

    //for usual ball:
    void OnCollisionEnter2D(Collision2D collisionInfo) {
        GameObject b = Instantiate(HalfBrokenBlock, new Vector2(gameObject.transform.position.x, 
                gameObject.transform.position.y), Quaternion.identity);
        b.transform.rotation = gameObject.transform.rotation;        
        Destroy(gameObject);
    }
}
