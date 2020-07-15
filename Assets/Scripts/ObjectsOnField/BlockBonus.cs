using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBonus : Block
{
  public GameObject Bonus;

  public override void OnHit() {
    GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CountBlock();
    GameObject.FindWithTag("CanvasGameInfo").GetComponent<CanvasGameInfo>().UpdateScore(SCORE_FOR_BLOCK);
    Instantiate(Bonus, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    Destroy(gameObject);
  }
}
