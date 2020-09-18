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
    public AudioClip catchCoin;
    public AudioClip catchBonus;
    public AudioClip catchDeatch;
    public AudioClip explosion;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        updateSoundPrefs();
    }

    public void updateSoundPrefs() 
    {
        hasSound = PlayerPrefs.GetInt("hasSound", 0);
        hasSound = 1; //for test
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
                audioSource.PlayOneShot(hitBlockSoundsPlasma[Random.Range(0, hitBlockSoundsPlasma.Length)]);
            } else {
                audioSource.PlayOneShot(hitSoundsPlasma[Random.Range(0, hitSoundsPlasma.Length)]);
            }
        }
    }

    public void playCatchCoin() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(catchCoin);
        }
    }

    public void playCatchBonus() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(catchBonus);
        }
    }

    public void playCatchDeath() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(catchDeatch);
        }
    }

    public void playExplosion() 
    {
        if (hasSound != 0) {
            audioSource.PlayOneShot(explosion);
        }
    }

}
