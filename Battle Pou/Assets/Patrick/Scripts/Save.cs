using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static Save instance;
    public SaveData saveData;
    public string path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.pou";
    public string key;

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
        }
        
    }
    public void SaveData()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.pou";
        List<float> listX = new();
        List<float> listZ = new();
        for (int i = 0; i < NewGeneration.instance.dungeonPositions.Count; i++)
        {
            listX.Add(NewGeneration.instance.dungeonPositions[i].position.x);
            listZ.Add(NewGeneration.instance.dungeonPositions[i].position.z);
        }
        List<int> ints = new();

        for (int i = 0; i < NewGeneration.instance.dungeons.Count; i++)
        {
            ints.Add(NewGeneration.instance.dungeons[i].GetComponent<Tile>().dungeonId);
        }
        saveData.dungeonType = ints.ToArray();
        saveData.dungeonX = listX.ToArray();
        saveData.dungeonZ = listZ.ToArray();
        //foreach (var v in InventoryManager.instance.items)
        //{
        //    saveData.inventoryIds.Add(v.GetComponent<ItemInfo>().id);
        //}
        saveData.health = PlayerHandler.Instance.hp;
        saveData.sp = PlayerHandler.Instance.sp;
        saveData.exp = PlayerHandler.Instance.exp;
        saveData.attackPower = PlayerHandler.Instance.attackPower;
        saveData.maxHp = PlayerHandler.Instance.maxHp;
        saveData.maxExp = PlayerHandler.Instance.maxExp;
        saveData.maxSp = PlayerHandler.Instance.maxSp;
        saveData.coins = PlayerHandler.Instance.coins;
        saveData.resolution = ResManager.instance.currentResolutionIndex;
        saveData.fpsLimit = Application.targetFrameRate;
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

    public void ClearData()
    {
        File.Delete(path);
    }
}
