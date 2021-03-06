﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

/*
Manage save and load, uses Save object
and another helper objects to save everything on the field.
Write save to save.save file.
*/

public class SaveLoadManager : MonoBehaviour
{
    public Ball ball;
    public BallPlasma ballPlasma;
    public Barrier barrier;
    public Canvas canvasBlocks;

    public Save CreateSaveObject(int currentLvl)
    {
      Save save = new Save();

      save.lvl = currentLvl;
      
      GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
      BallPlasma pb = (BallPlasma)FindObjectOfType(typeof(BallPlasma));
      foreach(GameObject ballToSave in allBalls)
      {
        if (! pb) {
          Ball b = ballToSave.GetComponent<Ball>();
          save.balls.Add(b.GetBallForSave());
        } else {
          BallPlasma b = ballToSave.GetComponent<BallPlasma>();
          save.balls.Add(b.GetBallForSave());
        }
      }

      GameObject racket = GameObject.FindGameObjectWithTag("Racket");
      if (racket != null) {
          save.racket = racket.GetComponent<Racket>().GetRacketForSave(); 
      } else {
          save.racket = null;
      }

      GameObject barrier = GameObject.FindGameObjectWithTag("Barrier");
      if (barrier != null) {
          save.barrier = barrier.GetComponent<Barrier>().GetBarrierForSave(); 
      } else {
          save.barrier = null;
      }
      

      GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
      foreach(GameObject block in allBlocks)
      {
        save.blocks.Add(block.GetComponent<Block>().GetBlockForSave());
      }

      GameObject[] allSteelBlocks = GameObject.FindGameObjectsWithTag("BlockSteel");
      foreach(GameObject blockSteel in allSteelBlocks)
      {
        save.blocksSteel.Add(blockSteel.GetComponent<BlockSteel>().GetBlockForSave());
      }

      GameObject[] allCoins = GameObject.FindGameObjectsWithTag("Coin");
      foreach(GameObject coin in allCoins)
      {
        save.coins.Add(coin.GetComponent<Coin>().GetCoinForSave());
      }

      GameObject[] allBonuses = GameObject.FindGameObjectsWithTag("Bonus");
      foreach(GameObject bonus in allBonuses)
      {
        save.bonuses.Add(bonus.GetComponent<Bonus>().GetBonusForSave());
      }

      GameObject[] allTelIn = GameObject.FindGameObjectsWithTag("TeleportIn");
      foreach(GameObject telIn in allTelIn)
      {
        save.telsIn.Add(telIn.GetComponent<TeleportIn>().GetTelInForSave());
      }

      GameObject[] allTelOut = GameObject.FindGameObjectsWithTag("TeleportOut");
      foreach(GameObject telOut in allTelOut)
      {
        save.telsOut.Add(telOut.GetComponent<TeleportOut>().GetTelOutForSave());
      }

      return save;
    }

    public void saveObjectsData(int currentLvl)
    {
      Save save = CreateSaveObject(currentLvl);
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Create(Application.persistentDataPath + "/save.save");
      bf.Serialize(file, save);
      file.Close();
      Debug.Log("Data saved");
    }

    public bool tryLoadObjectsData(int currentLvl)
    {
      GameObject gameManager = GameObject.FindWithTag("GameManager");

      if (File.Exists(Application.persistentDataPath + "/save.save")) {
        //Debug.Log(Application.persistentDataPath);
        Save save = null;
        try {
          BinaryFormatter bf = new BinaryFormatter();
          FileStream file = File.Open(Application.persistentDataPath + "/save.save", FileMode.Open);
          save = (Save)bf.Deserialize(file);
          file.Close();
          //Debug.Log("Data loaded");
        }
        catch {
          Debug.Log("Can't deserialize file");
          return false;
        }

        if (currentLvl != save.lvl) 
        {
          GameObject.FindWithTag("GameManager").GetComponent<GameManager>().setCurrentLvl(save.lvl);
          return false;
        }

        foreach(BallForSave ballToLoad in save.balls)
        {
          if (ballToLoad.plasmaBallDur <= 0) {
            Ball b = Instantiate(ball, new Vector2(ballToLoad.position_x, ballToLoad.position_y), 
                      Quaternion.identity);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(ballToLoad.velocity_x, 
                      ballToLoad.velocity_y);
            b.speed = ballToLoad.speed;
            b.gameObject.transform.localScale = new Vector3(ballToLoad.scale, ballToLoad.scale, 1);
          } else {
            BallPlasma pb = Instantiate(ballPlasma, new Vector2(ballToLoad.position_x, 
                      ballToLoad.position_y), Quaternion.identity);
            pb.GetComponent<Rigidbody2D>().velocity = new Vector2(ballToLoad.velocity_x, 
                      ballToLoad.velocity_y);
            pb.speed = ballToLoad.speed;
            pb.plasmaBallDuration = ballToLoad.plasmaBallDur;
            pb.gameObject.transform.localScale = new Vector3(ballToLoad.scale, ballToLoad.scale, 1);
          }
        }

        if (save.racket != null) {
          if (save.racket.len == 1) {
            GetComponent<Racket>().setShortRacket(save.racket.position_x, save.racket.position_y, false);
          } else if (save.racket.len == 3) {
            GetComponent<Racket>().setLongRacket(save.racket.position_x, save.racket.position_y, false);
          } else {
            GetComponent<Racket>().setNormalRacket(save.racket.position_x, save.racket.position_y, false);
          }
        }

        if (save.barrier != null) {
          Barrier b = Instantiate(barrier, new Vector2(0, -105), Quaternion.identity);
          b.barrierDuration = save.barrier.barrierDur;
        }
        GetComponent<SetBackground>().SetUpBackground(currentLvl);
        canvasBlocks.GetComponent<CanvasBlocksFromSave>().LoadBlocksFromSave(save.blocks, save.blocksSteel);
        GetComponent<CoinsFromSave>().LoadCoinsFromSave(save.coins);
        GetComponent<BonusesFromSave>().LoadBonusesFromSave(save.bonuses);
        GetComponent<TelsFromSave>().LoadTelsFromSave(save.telsIn, save.telsOut);
        return true;
        } else {
          return false;
        }
    }
}
