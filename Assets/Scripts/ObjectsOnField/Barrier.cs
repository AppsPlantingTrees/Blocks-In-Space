﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    float barrierDuration;
    bool isSemiTransparent = false;

    void Start()
    {
        //GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        //timer:
        //plasmaBallTimer = GameObject.Find("plasmaBallTimer");
        barrierDuration = PlayerPrefs.GetFloat("BarrierDuration", 5f);
    }

    void Update()
    {
      barrierDuration -= Time.deltaTime;

      if (barrierDuration <= 1.5f && barrierDuration > 0.0f && !isSemiTransparent)
      {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.5f);
        isSemiTransparent = true;
      }
      else if (barrierDuration <= 0.0f)
      {
        Destroy(gameObject);
      }

    }
}
