using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Main Block class, collision with Ball increases score and counter.
If ball is plasma, it calls OnTrigger and go through block (destroying it).
*/

public class Block : MonoBehaviour {
  public string typeOfBlock = "Block";
  public const int SCORE_FOR_BLOCK = 100;

  //for usual ball:
  void OnCollisionEnter2D(Collision2D collisionInfo) 
  {
    OnHit();
  }
  //for plasma ball:
  void OnTriggerEnter2D(Collider2D collisionInfo) 
  {
    OnHit();
  }

  public virtual void OnHit() 
  {
    GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CountBlock();
    GameObject.FindWithTag("CanvasGameInfo").GetComponent<CanvasGameInfo>().UpdateScore(SCORE_FOR_BLOCK);
    Destroy(gameObject);
  }

  public BlockForSave GetBlockForSave() 
  {
    return new BlockForSave() {
       position_x = transform.position.x,
       position_y = transform.position.y,
       type = typeOfBlock,
       rotation = transform.rotation.eulerAngles.z
    };
  }

}
