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
    private const int SHORT_RACKET = 1;
    private const int NORMAL_RACKET = 2;
    private const int LONG_RACKET = 3;
    public int lenRacket = NORMAL_RACKET;

    public Racket racket, shortRacket, longRacket;
  
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

  public void setShortRacket(float x, float y, bool needToDestroy)
  {
    Racket r = Instantiate(shortRacket, new Vector2(x, y), Quaternion.identity);
    r.lenRacket = SHORT_RACKET;
    if (needToDestroy) Destroy(gameObject);
  }  

  public void setNormalRacket(float x, float y, bool needToDestroy)
  {
    Racket r = Instantiate(racket, new Vector2(x, y), Quaternion.identity);
    r.lenRacket = NORMAL_RACKET;
    if (needToDestroy) Destroy(gameObject);
  } 

  public void setLongRacket(float x, float y, bool needToDestroy)
  {
    Racket r = Instantiate(longRacket, new Vector2(x, y), Quaternion.identity);
    r.lenRacket = LONG_RACKET;
    if (needToDestroy) Destroy(gameObject);
  }  

  public RacketForSave GetRacketForSave() 
  {
    return new RacketForSave() {
       position_x = transform.position.x,
       position_y = transform.position.y,
       len = lenRacket
    };
  }
}
