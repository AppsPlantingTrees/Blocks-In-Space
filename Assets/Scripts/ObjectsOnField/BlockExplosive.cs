using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockExplosive : Block
{
  public GameObject explosion;
  
  public override void OnHit() 
  {
      GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CountBlock();
      GameObject.FindWithTag("CanvasGameInfo").GetComponent<CanvasGameInfo>().UpdateScore(SCORE_FOR_BLOCK);

      Instantiate(explosion, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
      GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().playExplosion();

      Destroy(gameObject);
  }
}
