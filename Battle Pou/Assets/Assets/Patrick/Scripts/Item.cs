using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType { spell, weapon, consumable }
[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public ItemType type;
}
