using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ClearSaveData : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            File.Delete(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.pou");
        }
    }
}
