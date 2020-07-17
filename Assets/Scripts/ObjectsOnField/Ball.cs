using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public const float SPEED_SLOW = 60.0f;
    public const float SPEED_NORMAL = 100.0f;
    public const float SPEED_FAST = 150.0f;

    public const float MAX_SPEED = 300.0f;

    public const int SLOW_DOWN = 0;
    public const int SPEED_UP = 1;

    public float speed = SPEED_NORMAL;
    public int stuckCounter = 0;
    public float plasmaBallDuration = -1;

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
   }

  public void ChangeSpeed (int change) 
  {
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
  }

  public BallForSave GetBallForSave() 
  {
    return new BallForSave() {
       position_x = transform.position.x,
       position_y = transform.position.y,
       speed = this.speed,
       velocity_x = this.GetComponent<Rigidbody2D>().velocity.x,
       velocity_y = this.GetComponent<Rigidbody2D>().velocity.y,
       plasmaBallDur = plasmaBallDuration
    };
  }

}
