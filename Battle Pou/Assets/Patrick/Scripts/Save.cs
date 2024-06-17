using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static Save instance;
    public SaveData saveData;
    public string path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.pou";
    public string key;
    public bool mainMenu;

    public byte[] MakeKey()
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        string baseString = Convert.ToBase64String(keyBytes);
        byte[] aesKey = Encoding.UTF8.GetBytes(baseString);
        return aesKey;
    }
    public string EncryptString(string saveData)
    {
        byte[] iv = new byte[16];
        byte[] array;
        
        using (Aes aes = Aes.Create())
        {
            byte[] aesKey;
            aesKey = MakeKey();
            aes.Key = aesKey;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new())
            {
                using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new(cryptoStream))
                    {
                        streamWriter.Write(saveData);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }
    public string DecryptString(string encryptedJson)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(encryptedJson);

        using (Aes aes = Aes.Create())
        {
            byte[] aesKey;
            aesKey = MakeKey();
            aes.Key = aesKey;
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
    private void Start()
    {
        instance = this;
        saveData = LoadData();
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.pou";
        if (File.Exists(path))
        {
            FindAnyObjectByType<Load>().Initialize(saveData);
            FindAnyObjectByType<StatsChangeOverworld>().Change();
        }
        StartCoroutine(AutoSave());
        
    }
    public void SaveData()
    {
        if (!mainMenu)
        {
            Debug.Log("saved stats");
            saveData.inventoryIds.Clear();
            saveData.inventoryCount.Clear();
            path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.pou";
            List<float> listX = new();
            List<float> listZ = new();
            if (NewGeneration1.instance.dungeonPositions != null)
            {
                for (int i = 0; i < NewGeneration1.instance.dungeonPositions.Count; i++)
                {
                    listX.Add(NewGeneration1.instance.dungeonPositions[i].position.x);
                    listZ.Add(NewGeneration1.instance.dungeonPositions[i].position.z);
                }
            }
            List<int> ints = new();
            for (int i = 0; i < NewGeneration1.instance.rooms.Count; i++)
            {
                ints.Add(NewGeneration1.instance.rooms[i].GetComponent<Tile1>().dungeonId);
            }
            saveData.dungeonType = ints.ToArray();
            saveData.dungeonX = listX.ToArray();
            saveData.dungeonZ = listZ.ToArray();
            foreach (var v in InventoryManager.instance.items)
            {
                saveData.inventoryIds.Add(v.GetComponentInChildren<ItemInfo>().id);
                saveData.inventoryCount.Add(v.GetComponentInChildren<ItemInfo>().count);
            }
            saveData.health = PlayerHandler.Instance.hp;
            saveData.sp = PlayerHandler.Instance.sp;
            saveData.exp = PlayerHandler.Instance.exp;
            saveData.attackPower = PlayerHandler.Instance.attackPower;
            saveData.maxHp = PlayerHandler.Instance.maxHp;
            saveData.maxExp = PlayerHandler.Instance.maxExp;
            saveData.maxSp = PlayerHandler.Instance.maxSp;
            saveData.coins = PlayerHandler.Instance.coins;
            saveData.level = PlayerHandler.Instance.level;
        }
        saveData.resolution = ResManager.instance.resolutionDropdown[0].value;
        if (Application.targetFrameRate == -1)
        {
            saveData.fpsLimit = 0;
        }
        else
        {
            saveData.fpsLimit = Application.targetFrameRate;
        }
        saveData.fullScreen = Screen.fullScreen;
        saveData.volume = AudioListener.volume;
        string json = JsonUtility.ToJson(saveData);
        Debug.Log(json);
        string encryptedJson = EncryptString(json);

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
            string decodedJson = DecryptString(json);

            data = JsonUtility.FromJson<SaveData>(decodedJson);
        }
        else
        {
            data = new SaveData();
        }
        return data;
    }

    IEnumerator AutoSave()
    {
        yield return new WaitForSeconds(5);
        SaveData();
        Debug.Log("Autosaved");
        StartCoroutine(AutoSave());
    }
    public void ClearData()
    {
        File.Delete(path);
    }
}
