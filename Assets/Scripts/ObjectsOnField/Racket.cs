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
    float speed = 20f;
    float screenCenterX;
    Vector3 direction;
    Vector3 touchPosition;
    Rigidbody2D rb;
  

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update () 
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = touchPosition - transform.position;
            rb.velocity = new Vector2(direction.x, 0) * speed;

            if(touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
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
