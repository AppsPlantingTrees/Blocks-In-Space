﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPlasma : Bonus
{
  public GameObject plasmaBall;

   public override void GetBonus()
   {
       //makes all balls plasma
       //start of plasma ball make all bricks cuttable
       GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
       BallPlasma pb = (BallPlasma)FindObjectOfType(typeof(BallPlasma));
       if (!pb) {
        foreach(GameObject ball in allBalls)
        {
          GameObject obj = Instantiate(plasmaBall, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
          obj.GetComponent<Rigidbody2D>().velocity = ball.GetComponent<Rigidbody2D>().velocity;
          Vector3 scale = new Vector3(ball.gameObject.transform.localScale.x*130, ball.gameObject.transform.localScale.y*130, 1);
          obj.gameObject.transform.localScale = scale;
          Destroy(ball.gameObject);
        }
       } else {
          float d = PlayerPrefs.GetFloat("PlasmaBallDuration", 5f);
          foreach(GameObject ball in allBalls)
          {
            ball.GetComponent<BallPlasma>().plasmaBallDuration = d;
          }
       }
   }
}
