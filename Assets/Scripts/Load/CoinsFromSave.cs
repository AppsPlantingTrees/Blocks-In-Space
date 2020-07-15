using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsFromSave : MonoBehaviour
{
    public Coin coin;

    public void LoadCoinsFromSave(List<MovingObjectForSave> allCoins)
    {
        foreach(MovingObjectForSave coinToLoad in allCoins)
        {
            Coin c = Instantiate(coin, new Vector2(coinToLoad.position_x, 
                    coinToLoad.position_y), Quaternion.identity);
            c.GetComponent<Rigidbody2D>().velocity = new Vector2(coinToLoad.velocity_x, 
                    coinToLoad.velocity_y);
        }
    }
}
