using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Save : MonoBehaviour
{
    public SaveData saveData;
    public string path;
    private void Start()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        saveData = LoadData();
    }
    public void SaveDungeons()
    {
        List<float> listX = new List<float>();
        List<float> listZ = new List<float>();
        for (int i = 0; i < Generation.instance.dungeonPositions.Count - 1; i++)
        {
            listX.Add(Generation.instance.dungeonPositions[i].x);
            listZ.Add(Generation.instance.dungeonPositions[i].z);
        }
        saveData.dungeonX = listX.ToArray();
        saveData.dungeonZ = listZ.ToArray();
        //player health
        //player sp
        //dungeon type
        //(these havent been implemented yet as of writing this)
        string json = JsonUtility.ToJson(saveData);
        Debug.Log(json);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        string encryptedJson = Convert.ToBase64String(bytes);

        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(encryptedJson);
        }
    }

    SaveData LoadData()
    {
        string json;
        SaveData data;
        if (File.Exists(path))
        {
            using (StreamReader sr = new StreamReader(path))
            {
                json = sr.ReadToEnd();
            }
            byte[] decodedBytes = Convert.FromBase64String(json);
            string decodedJson = Encoding.UTF8.GetString(decodedBytes);

            data = JsonUtility.FromJson<SaveData>(decodedJson);
        }
        else
        {
            data = new SaveData();
        }
        return data;
    }

}
