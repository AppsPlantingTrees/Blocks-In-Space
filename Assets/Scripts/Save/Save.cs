using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Save object, which contains info about all objects on the field.
*/

[System.Serializable]
public class Save
{
  public List<BallForSave> balls = new List<BallForSave>();
  public int lvl;
  public List<MovingObjectForSave> coins = new List<MovingObjectForSave>();
  public List<BonusForSave> bonuses = new List<BonusForSave>();
  public ObjectForSave racket;
  public List<BlockForSave> blocks = new List<BlockForSave>();
  public List<BlockForSave> blocksSteel = new List<BlockForSave>();
  public List<ObjectForSave> telsIn = new List<ObjectForSave>();
  public List<ObjectForSave> telsOut = new List<ObjectForSave>();
}