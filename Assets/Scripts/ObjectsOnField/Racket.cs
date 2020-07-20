using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Racket : MonoBehaviour
{
    /*public float speedPC = 150;
    //bool isMagnet = false;
    //GameObject ball;

    void FixedUpdate()
    {
        // Get Horizontal Input
        float h = Input.GetAxisRaw("Horizontal");

        // Set Velocity (movement direction * speed)
        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * speedPC;

        //if (isMagnet) {
        //  ball.GetComponent<Rigidbody>().isKinematic = true;
        //}
    }*/

    /*void OnCollisionEnter2D(Collision2D collisionInfo)
    {
      //if (collisionInfo.gameObject.tag == "Ball" && isMagnet) {
      //  ball = collisionInfo.gameObject;
      //  ball.GetComponent<Rigidbody>().isKinematic = false;
      //}
    }*/

    /*public void MagnetRacket()
    {
      isMagnet = true;
    }*/
    

    //FOR ANDROID:
    private float speed = 10f;
    private Vector3 direction, touchPosition;
    private Rigidbody2D RacketRB;
  

    private void Start()
    {
        RacketRB = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
      if(Input.touchCount > 0)
      {
          Touch touch = Input.GetTouch(0);
          touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
          if (touchPosition.y < 0)
          {
            direction = touchPosition - transform.position;
            RacketRB.velocity = new Vector2(direction.x, 0) * speed;

            if(touch.phase == TouchPhase.Ended)
            {
                RacketRB.velocity = Vector2.zero;
            }
          }
        }
    }

  public ObjectForSave GetRacketForSave() 
  {
    return new ObjectForSave() {
       position_x = transform.position.x,
       position_y = transform.position.y,
    };
  }

}
