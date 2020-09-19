using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWinGame : MonoBehaviour
{
    public GameObject firework, textGameScore;
    private const int NUMBER_OF_FIREWORKS = 7;
    private GameObject[] fireworks = new GameObject[NUMBER_OF_FIREWORKS];
    private float half_width, half_heigth;
    

    public void SetUpCanvas(int score)
    {
        textGameScore.GetComponent<Text>().text = "SCORE: " + score;

        Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        half_width = world.x - 10;
        half_heigth = world.y - 10;

        //start 5 fireworks:
        for (int i = 0; i < NUMBER_OF_FIREWORKS; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-1*half_width, half_width), 
                    Random.Range(-1*half_heigth, half_heigth));
            fireworks[i] = Instantiate(firework, pos, Quaternion.identity);

            var main = fireworks[i].GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }
    }

    //OnStop simply place firework on different postion and play it again:
    public void OnParticleSystemStopped()
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        half_width = world.x - 10;
        half_heigth = world.y - 10;

        this.transform.position = new Vector2(Random.Range(-1*half_width, half_width), 
                Random.Range(-1*half_heigth, half_heigth));

        var emission = GetComponent<ParticleSystem>().emission;
        emission.enabled = true;
        GetComponent<ParticleSystem>().Play();
    }

    //Stop and destroy everything on pressing new game:
    public void OnNewGamePressed()
    {
        for (int i = 0; i < NUMBER_OF_FIREWORKS; i++)
        {
            if (fireworks[i].GetComponent<ParticleSystem>().isPlaying)
            {
                fireworks[i].GetComponent<ParticleSystem>().Stop();
            }
            Destroy(fireworks[i]);
        }
        gameObject.SetActive(false);
    }

}
