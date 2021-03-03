using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType 
{
    Tool,
    Consumable,
    Key,
}


[System.Serializable]
public class Item
{
    protected static int itemGlobalCounter = 0;
    protected int itemID;

    [SerializeField]
    protected string name;
    [SerializeField]
    public Sprite sprite;

    //public Sprite Sprite { get; }

    //public ItemType itemType;

    public Item()
    {

    }
    
    public Item(string definedName, Sprite definedSprite)
    {
        itemGlobalCounter += 1;
        this.itemID = itemGlobalCounter;
        this.name = definedName;
        this.sprite = definedSprite;
    }
}
