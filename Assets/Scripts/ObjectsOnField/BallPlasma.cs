using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallPlasma : Ball
{
  public GameObject ball;
  //GameObject plasmaBallTimer;

  void Start() 
  {
      //makes all bricks not collidable (ball will go through and destroy them)
      GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
      foreach(GameObject block in allBlocks)
      {
        block.GetComponent<BoxCollider2D>().isTrigger = true;
      }
      //makes all steel bricks not collidable too
      GameObject[] allSteelBlocks = GameObject.FindGameObjectsWithTag("BlockSteel");
      foreach(GameObject blockSteel in allSteelBlocks)
      {
        blockSteel.GetComponent<PolygonCollider2D>().isTrigger = true;
      }
      //timer:
      //plasmaBallTimer = GameObject.Find("plasmaBallTimer");
      plasmaBallDuration = PlayerPrefs.GetFloat("PlasmaBallDuration", 5f);
  }

  void Update() {

   plasmaBallDuration -= Time.deltaTime;

   //update timer in the bottom of the screen:
   //plasmaBallTimer.GetComponent<Text>().text = plasmaBallDuration.ToString();

   if (plasmaBallDuration <= 0.0f)
   {
     //makes all balls not plasma
     GameObject[] allPlasmaBalls = GameObject.FindGameObjectsWithTag("Ball");
     foreach(GameObject plasmaBall in allPlasmaBalls)
     {
       GameObject obj = Instantiate(ball, new Vector2(plasmaBall.transform.position.x, plasmaBall.transform.position.y), Quaternion.identity);
       obj.GetComponent<Rigidbody2D>().velocity = plasmaBall.GetComponent<Rigidbody2D>().velocity;
       Destroy(plasmaBall.gameObject);
     }

     //makes all bricks collidable
     GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
     foreach(GameObject block in allBlocks)
     {
       block.GetComponent<BoxCollider2D>().isTrigger = false;
     }

      //makes all steel bricks collidable
     GameObject[] allSteelBlocks = GameObject.FindGameObjectsWithTag("BlockSteel");
     foreach(GameObject blockSteel in allSteelBlocks)
     {
       blockSteel.GetComponent<PolygonCollider2D>().isTrigger = false;
     }

     //clean timer:
     //plasmaBallTimer.GetComponent<Text>().text = "";

   }
 }
}
