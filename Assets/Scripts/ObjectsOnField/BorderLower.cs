using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLower : MonoBehaviour
{
  public GameObject ball;
  public GameObject racket;

  void OnCollisionEnter2D(Collision2D collisionInfo) {

      if (collisionInfo.gameObject.tag == "Ball") {
        GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject[] allPlasmaBalls = GameObject.FindGameObjectsWithTag("PlasmaBall");

        if (allBalls.Length == 1 || allPlasmaBalls.Length == 1)
        {
          //make all blocks collidable again:
          GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
          foreach(GameObject block in allBlocks)
          {
            block.GetComponent<BoxCollider2D>().isTrigger = false;
          }
          GameObject[] allSteelBlocks = GameObject.FindGameObjectsWithTag("BlockSteel");
          foreach(GameObject blockSteel in allSteelBlocks)
          {
            blockSteel.GetComponent<PolygonCollider2D>().isTrigger = false;
          }

          Destroy(collisionInfo.gameObject);
          
          //if not last ball, install ball again:
          if (GameObject.FindWithTag("CanvasGameInfo").GetComponent<CanvasGameInfo>().LivesMinus()) {
            Instantiate(ball, new Vector2(0, -85), Quaternion.identity);
          }

          //install new racket:
          Destroy(GameObject.FindWithTag("Racket"));
          Instantiate(racket, new Vector2(0, -95), Quaternion.identity);
        }
        else
        {
          Destroy(collisionInfo.gameObject);
        }
    }
    else
    {
      Destroy(collisionInfo.gameObject);
    }
  }
}
