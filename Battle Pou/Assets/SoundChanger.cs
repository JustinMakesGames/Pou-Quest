using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChanger : MonoBehaviour
{
    public void OnValueChanged(float value)
    {
        AudioListener.volume = value;
    }
}
