using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ITEM
{
    public string itemName;
    public ItemType itemType;

    public enum ItemType
    {
        HealthPotion,
        Firebomb,
        GelBomb,
        MushroomBomb
    }
}
