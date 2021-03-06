﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallPlasma : Ball
{
  //private GameObject soundManager;

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

      //field bounds:
      world = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
      halfSizeBall = GetComponent<Renderer>().bounds.size.x / 2;
      
      soundManager = GameObject.FindGameObjectWithTag("SoundManager");
  }

  void OnTriggerEnter2D(Collider2D collisionInfo) {
    soundManager.GetComponent<SoundManager>().playHitPlasmaSound(collisionInfo.gameObject.tag);
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
       Vector3 scale = new Vector3(plasmaBall.gameObject.transform.localScale.x/130, plasmaBall.gameObject.transform.localScale.y/130, 1);
       obj.gameObject.transform.localScale = scale;
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
