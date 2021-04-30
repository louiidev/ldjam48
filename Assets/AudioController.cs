using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource music, fx;
    public AudioClip music1, music2;
    public AudioClip hitFX, fireFX, fireFX2, glassFX, explosionFX, deathFX, pickupItemFX;
    public AudioClip[] walksFX;

    public void ChangeMusic(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

    public void PlayFX(AudioClip clip)
    {
        fx.PlayOneShot(clip);
    }

    public AudioClip RandomWalk()
    {
        return walksFX[Random.Range(0, walksFX.Length)];
    } 
}
