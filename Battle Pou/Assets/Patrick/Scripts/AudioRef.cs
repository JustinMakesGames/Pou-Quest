using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRef : MonoBehaviour
{
    public AudioSource[] bottleAudios;
    public AudioSource[] damageAudios;
    public AudioSource swing;
    public AudioSource ambient;
    public static AudioRef instance;
    public AudioSource levelUp;
    public AudioSource laser;
    public AudioSource ghost;
    public AudioSource[] dialog;

    public void Awake()
    {
        instance = this;
    }
}
