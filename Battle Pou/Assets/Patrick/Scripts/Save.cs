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
    public string path;
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

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
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
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.pou";
        saveData = LoadData();
        if (File.Exists(path))
        {
            FindAnyObjectByType<Load>().Initialize(saveData);
        }
        
    }
    public void SaveDungeons()
    {
        List<float> listX = new List<float>();
        List<float> listZ = new List<float>();
        for (int i = 0; i < Generation.instance.dungeonPositions.Count; i++)
        {
            listX.Add(Generation.instance.dungeonPositions[i].x);
            listZ.Add(Generation.instance.dungeonPositions[i].z);
        }
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
}
