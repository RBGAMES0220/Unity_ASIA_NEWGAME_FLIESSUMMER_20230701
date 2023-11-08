using UnityEngine;

namespace GLORY
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
    public class Item : ScriptableObject
    {
        public int id;
        public int value;
        public string itemName;
        public Sprite itemImage;
        public int itemHeld;
        [TextArea]
        public string itemInfo;
        public bool equip;
        
    }
}
