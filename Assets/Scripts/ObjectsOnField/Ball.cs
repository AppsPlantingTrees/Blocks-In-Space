﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public GameObject soundManager;

    public const float SPEED_SLOW = 60.0f;
    public const float SPEED_NORMAL = 80.0f;
    public const float SPEED_FAST = 120.0f;

    public const float MAX_SPEED = 160.0f;

    public const int SLOW_DOWN = 0;
    public const int SPEED_UP = 1;

    public float speed = SPEED_NORMAL;
    public int stuckCounter = 0;
    public float plasmaBallDuration = -1;

    public Vector3 ballPosScreen;
    public Vector3 world;
    public float halfSizeBall;

    void Start() 
    {
      world = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
      halfSizeBall = GetComponent<Renderer>().bounds.size.x / 2;
      soundManager = GameObject.FindGameObjectWithTag("SoundManager");
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        Rigidbody2D ballRb = GetComponent<Rigidbody2D>();
        if (collisionInfo.gameObject.tag == "Racket") {
            stuckCounter = 0;
            float x = (transform.position.x - collisionInfo.transform.position.x) 
                            / collisionInfo.collider.bounds.size.x;

            Vector2 dir = new Vector2(x, 1).normalized;
            ballRb.velocity = dir * speed;
        }
        stuckCounter++;
        //if ball stuck somewhere, change it's velocity a bit:
        if (stuckCounter > 15) {
          ballRb.velocity = new Vector2(ballRb.velocity.x + Random.Range(-10, 10), 
                  ballRb.velocity.y + Random.Range(-10, 10));        
        }
        soundManager.GetComponent<SoundManager>().playHitSound(collisionInfo.gameObject.tag);
    }

  float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth) 
  {
    return (ballPos.x - racketPos.x) / racketWidth;
  }

  void FixedUpdate()
   {
      //keep speed under MAX_SPEED:
      Rigidbody2D ballRb = GetComponent<Rigidbody2D>();
      if(ballRb.velocity.magnitude > MAX_SPEED)
      {
        ballRb.velocity = ballRb.velocity.normalized * MAX_SPEED;
      }

      //keep ball within the field:
      ballPosScreen = transform.position;
      if (ballPosScreen.x >= world.x) 
      {
        //Debug.Log("ball off screen: " + ballPosScreen);
        ballPosScreen.x = world.x - halfSizeBall;
        transform.position = ballPosScreen;
      } else if(ballPosScreen.x <= -world.x)
      {
        //Debug.Log("ball off screen: " + ballPosScreen);
        ballPosScreen.x = -(world.x - halfSizeBall);
        transform.position = ballPosScreen;
      }
   }

  public void ChangeSpeed (int change) 
  {
    Rigidbody2D ballRb = GetComponent<Rigidbody2D>();
    Vector2 direction = new Vector2(ballRb.velocity.x, ballRb.velocity.y).normalized;
    if (change == SPEED_UP) 
    {
      if (speed == SPEED_SLOW) {
        speed = SPEED_NORMAL;
      } else {
        speed = SPEED_FAST;
      }
    } else if (change == SLOW_DOWN) {
      if (speed == SPEED_FAST) {
        speed = SPEED_NORMAL;
      } else {
        speed = SPEED_SLOW;
       }
    }
    ballRb.velocity = direction * speed;
  }

  public BallForSave GetBallForSave() 
  {
    return new BallForSave() {
       position_x = transform.position.x,
       position_y = transform.position.y,
       speed = this.speed,
       scale = transform.localScale.x,
       velocity_x = this.GetComponent<Rigidbody2D>().velocity.x,
       velocity_y = this.GetComponent<Rigidbody2D>().velocity.y,
       plasmaBallDur = plasmaBallDuration
    };
  }

}
