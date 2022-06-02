using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] AudioClip powerupSFX;
    AudioSource audioPlayer;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        if (audioPlayer == null) { Debug.LogError("Audio Manager Audio Source is Null"); }
    }

    public void PlayExplosionSoundSFX()
    {
        audioPlayer.PlayOneShot(explosionSFX);
    }

    public void PlayLaserSoundSFX()
    {
        audioPlayer.PlayOneShot(laserSFX);
    }

    public void PlayPowerupSFX()
    {
        audioPlayer.PlayOneShot(powerupSFX);
    }
}