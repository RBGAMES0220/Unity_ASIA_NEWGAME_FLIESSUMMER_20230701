using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLORY;

namespace GLORY
{
    public class CraftingSystem : MonoBehaviour
    {
        public Inventory playerInventory; // 玩家的背包
        public List<CraftingRecipe> craftingRecipes; // 可用的合成規則

        // 玩家嘗試合成物品
        public void CraftItem(Item item1, Item item2)
        {
            // 檢查是否存在匹配的合成規則
            CraftingRecipe matchingRecipe = GetMatchingRecipe(item1, item2);

            if (matchingRecipe != null)
            {
                // 檢查玩家是否擁有足夠的原材料
                if (playerInventory.itemList.Contains(item1) && playerInventory.itemList.Contains(item2))
                {
                    // 從背包中移除原材料
                    playerInventory.itemList.Remove(item1);
                    playerInventory.itemList.Remove(item2);

                    // 將合成結果添加到背包中
                    playerInventory.itemList.Add(matchingRecipe.result);

                    // 更新 UI 以反映背包中的變化
                    InventoryManager.RefreshItem();
                }
            }
            else
            {
                // 顯示無法合成的提示或錯誤信息
            }
        }

        // 查找匹配的合成規則
        private CraftingRecipe GetMatchingRecipe(Item item1, Item item2)
        {
            foreach (CraftingRecipe recipe in craftingRecipes)
            {
                if ((recipe.ingredient1 == item1 && recipe.ingredient2 == item2) ||
                    (recipe.ingredient1 == item2 && recipe.ingredient2 == item1))
                {
                    return recipe;
                }
            }

            return null; // 找不到匹配的合成規則
        }
    }
}
