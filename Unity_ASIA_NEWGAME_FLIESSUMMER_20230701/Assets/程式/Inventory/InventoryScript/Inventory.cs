using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace GLORY
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
    public class Inventory : ScriptableObject
    {
        public List<Item> itemList = new List<Item>();
    }
}
