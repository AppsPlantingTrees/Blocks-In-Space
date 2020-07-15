using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockYellow : Block
{
  public GameObject coin;

  public override void OnHit() {
      GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CountBlock();
      GameObject.FindWithTag("CanvasGameInfo").GetComponent<CanvasGameInfo>().UpdateScore(SCORE_FOR_BLOCK);
      int numberOfCoins = Random.Range(3, 6);
      for (int i = 0; i < numberOfCoins; i++)
      {
        Instantiate(coin, new Vector2(transform.position.x + Random.Range(-7, 0)*i, 
                transform.position.y + Random.Range(-7, 0)*i*2), Quaternion.identity);
      }

      Destroy(gameObject);
  }

}
