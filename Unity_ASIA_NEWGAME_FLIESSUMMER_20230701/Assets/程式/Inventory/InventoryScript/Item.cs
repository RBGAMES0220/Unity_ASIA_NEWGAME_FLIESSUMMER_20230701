using UnityEngine;
using GLORY;

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
        public bool isUsable; // 新增是否可用屬性
        public bool isCraftable; // 增加一個屬性，標識是否可以合成
        public CraftingRecipe craftingRecipe; // 存儲合成資訊的參考

       

        public void UseItem()
        {
            // 在這裡定義物品的使用效果
            if (isUsable)
            {
                if (itemName == "Health Potion")
                {
                    // 增加玩家的生命值
                    // 這裡需要您實現玩家的生命值管理邏輯
                }
                // 添加其他可使用物品的效果
            }
        }
    }
}
