using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip fireBiome;
    public AudioClip waterBiome;
    public AudioClip lightBiome;
    public AudioClip darkBiome;
    public AudioClip earthBiome;
    public AudioClip windBiome;

    public void SetAudioClip(AudioClip clip)
    {
        this.GetComponent<AudioSource>().clip = clip;
        this.GetComponent<AudioSource>().Play();
    }
}
