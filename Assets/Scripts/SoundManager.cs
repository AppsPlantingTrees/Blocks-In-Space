using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    //TODO add another audioSource for music later
    private int hasSound;

    public AudioClip[] hitSounds;
    public AudioClip[] hitBlockSounds;
    public AudioClip[] hitSteelBlockSounds;
    public AudioClip[] hitSoundsPlasma;
    public AudioClip[] hitBlockSoundsPlasma;
    public AudioClip[] catchCoin;
    public AudioClip catchBonus;
    public AudioClip catchDeatch;
    public AudioClip lostBall;
    public AudioClip[] explosionSounds;

    public AudioClip winLvl;
    public AudioClip winGame;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        updateSoundPrefs();
    }

    public void updateSoundPrefs() 
    {
        hasSound = PlayerPrefs.GetInt("hasSound", 0);
        //hasSound = 1; //for test
    }

    public void playHitSound(string tag) 
    {
        if (hasSound != 0) {
            if (tag == "Block") {
                audioSource.PlayOneShot(hitBlockSounds[Random.Range(0, hitBlockSounds.Length)]);
            } else if (tag == "BlockSteel") {
                audioSource.PlayOneShot(hitSteelBlockSounds[Random.Range(0, hitSteelBlockSounds.Length)]);
            } else {
                audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
            }
        }
    }

    public void playHitPlasmaSound(string tag) 
    {
        if (hasSound != 0) {
            if (tag == "Block" || tag == "BlockSteel") {
                audioSource.PlayOneShot(hitBlockSoundsPlasma[Random.Range(0, hitBlockSoundsPlasma.Length)], 0.5f);
            } else {
                audioSource.PlayOneShot(hitSoundsPlasma[Random.Range(0, hitSoundsPlasma.Length)], 0.5f);
            }
        }
    }

    public void playCatchCoin() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(catchCoin[Random.Range(0, catchCoin.Length)], 0.1f);
        }
    }

    public void playCatchBonus() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(catchBonus, 0.3f);
        }
    }

    public void playCatchDeath() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(catchDeatch, 0.4f);
        }
    }

    public void playLostBall() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(lostBall, 0.6f);
        }
    }

    public void playExplosion() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(explosionSounds[Random.Range(0, explosionSounds.Length)], 0.8f);
        }
    }

    public void playWinLvl() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(winLvl, 0.5f);
        }
    }

    public void playWinGame() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(winGame, 0.5f);
        }
    }

    public void stopPlaying() 
    {
        audioSource.Stop();
    }

}
