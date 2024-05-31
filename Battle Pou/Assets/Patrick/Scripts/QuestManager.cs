using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public string currentQuest;
    public TMP_Text questText;
    private void Start()
    {
        instance = this;
    }

    bool IsCharNumber(char character)
    {
        return Char.IsNumber(character);
        
    }
    public void UpdateQuest()
    {
        //int i = 0;
        //foreach (char c in currentQuest.ToCharArray())
        //{
        //    bool isNumber = char.IsNumber(c);
        //    if (isNumber)
        //    {
        //        int num = currentQuest.IndexOf(c);
        //        currentQuest = currentQuest.Replace(currentQuest[num], Convert.ToChar(Convert.ToInt32(c) - 1));
        //        questText.text = currentQuest;
        //        i++;
        //        break;
        //    }
        //    if (i == 1)
        //    {
        //        break;
        //    }
        //}
        foreach (char c in currentQuest)
        {
            currentQuest = "";
            bool isNumber = IsCharNumber(c);
            if (isNumber)
            {
                int num = currentQuest.IndexOf(c);
                char[] chars = currentQuest.ToCharArray();
                bool isNextNumberAlsoNumber = false;
                try { isNextNumberAlsoNumber = IsCharNumber(chars[num + 1]); }
                catch { isNextNumberAlsoNumber = false; }
                if (isNextNumberAlsoNumber)
                {
                    int numToChange = (c * 10) + chars[num + 1];
                    numToChange--;
                    
                    string[] ints = numToChange.ToString().Split(new char[] { ' ' }, 2);
                    chars[num] = Convert.ToChar(ints[0]);
                    chars[num + 1] = Convert.ToChar(ints[1]);
                    foreach (char c2 in chars)
                    {
                        currentQuest += c2;
                    }
                }
                else
                {
                    int numToChange = Convert.ToInt32(c);
                    numToChange--;
                    int index = currentQuest.IndexOf(c);
                    chars[index] = Convert.ToChar(numToChange);
                    foreach (char c2 in chars)
                    {
                        currentQuest += c2;
                    }
                }
            }
        }
    }

    public void AddQuest()
    {
        if (currentQuest != null)
        {
            currentQuest = QuestGenerator.Instance.GenerateQuest();
            questText.text = currentQuest;
        }
        else
        {
            Debug.Log("You already have a quest");
        }
    }
}
