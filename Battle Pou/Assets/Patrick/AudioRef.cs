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

    public void Awake()
    {
        instance = this;
    }
}
