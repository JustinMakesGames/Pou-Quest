using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public int hp;
    public int sp;
}
